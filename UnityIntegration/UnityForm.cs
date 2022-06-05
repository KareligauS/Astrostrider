using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Astrostrider.UnityIntegration
{
    public partial class UnityForm : Form, IDisposable
    {
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        #region Fields

        private Process movProcess;
        private string _unityExePath;

        private IntPtr unityHWND = IntPtr.Zero;

        private const int WM_ACTIVATE = 0x0006;
        private const int WM_INPUT = 0x00FF;
        private const int GWLP_WNDPROC = -4;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public IntPtr hwndTarget;
        }

        const ushort HID_USAGE_PAGE_GENERIC = 0x01;
        const ushort HID_USAGE_GENERIC_KEYBOARD = 0x06;

        private new delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private MulticastDelegate originalWndProc;
        private WndProc myWndProc;

        #endregion Fields

        #region Properties

        [DllImport("User32.dll")]
        private static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);

        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SetParent(IntPtr hWnd, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] RAWINPUTDEVICE[] pRawInputDevices, int uiNumDevices, int cbSize);

        public string UnityExePath { get { return _unityExePath; } set { _unityExePath = value; } }

        #endregion Properties

        #region Constructors

        public UnityForm(string ExePath)
        {
            InitializeComponent();

            TopLevel = false;

            try
            {
                UnityExePath = ExePath;
                movProcess = new Process();
                movProcess.StartInfo.FileName = $"{UnityExePath}";
                movProcess.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32();
                movProcess.StartInfo.UseShellExecute = true;
                movProcess.StartInfo.CreateNoWindow = true;
                movProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                movProcess.Start();
                movProcess.WaitForInputIdle();

                EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);

                SetupRawInput(panel1.Handle);
            }
            catch (Exception lovException)
            {
                movProcess.Kill();
                MessageBox.Show(lovException.Message);
                ActiveForm.Close();
            }
        }

        #endregion Constructors

        #region Methods

        public void ActivateUnityWindow()
        {
            SendMessage(unityHWND, WM_ACTIVATE, WA_ACTIVE, IntPtr.Zero);
            TopLevel = false;
            SetForegroundWindow(unityHWND);
        }

        private void DeactivateUnityWindow()
        {
            SendMessage(unityHWND, WM_ACTIVATE, WA_INACTIVE, IntPtr.Zero);
        }
        private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            unityHWND = hwnd;
            ActivateUnityWindow();
            return 0;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            MoveWindow(unityHWND, 0, 0, panel1.Width, panel1.Height, true);
            ActivateUnityWindow();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            /*
            CSWin32.SetParent(unityHWND, IntPtr.Zero);
            CSWin32.SetWindowLong(unityHWND, CSWin32.GWL_STYLE, CSWin32.WS_VISIBLE);
            CSWin32.MoveWindow(unityHWND, 0, 0, 800, 600, true);
            CSWin32.SetForegroundWindow(unityHWND);
            CSWin32.ShowWindow(unityHWND, 2);
            Thread.Sleep(100);

            CSWin32.PostMessage(unityHWND, CSWin32.WM_CLOSE, 0, 0);
            */

            //Work in progress
            //SetParent(unityHWND, IntPtr.Zero);
            //SetForegroundWindow(unityHWND);
            //ShowWindow(unityHWND, 2);
            //Thread.Sleep(100);

            //PostMessage(unityHWND, WM_CLOSE, WA_INACTIVE, IntPtr.Zero);

            base.OnClosing(e);
        }

        private IntPtr HookWndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == WM_INPUT)
            {
                SendMessage(unityHWND, msg, wParam, lParam);
                return IntPtr.Zero;
            }

            return (IntPtr)originalWndProc.DynamicInvoke(new object[] { hwnd, msg, wParam, lParam });
        }

        private void SetupRawInput(IntPtr hostHWND)
        {
            myWndProc = HookWndProc;

            IntPtr originalWndProcPtr = SetWindowLongPtr(hostHWND, GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(myWndProc));
            if (originalWndProcPtr == null)
            {
                var errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, "Failed to overwrite the original wndproc");
            }

            Type lel = typeof(MulticastDelegate);
            originalWndProc = (MulticastDelegate)Marshal.GetDelegateForFunctionPointer(originalWndProcPtr, lel);

            var rawInputDevices = new[]
            {
                new RAWINPUTDEVICE()
                {
                    usUsagePage = HID_USAGE_PAGE_GENERIC,
                    usUsage = HID_USAGE_GENERIC_KEYBOARD,
                    dwFlags = 0,
                    hwndTarget = hostHWND
                }
            };

            if (!RegisterRawInputDevices(rawInputDevices, 1, Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
            {
                var lastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(lastError, "Failed to register raw input devices");
            }
        }

        #endregion Methods

        #region Event Handlers

        // Close Unity application
        private void UnityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                movProcess.CloseMainWindow();

                Thread.Sleep(1000);
                while (movProcess.HasExited == false)
                    movProcess.Kill();
            }
            catch (Exception)
            {

            }
        }

        private void UnityForm_Activated(object sender, EventArgs e)
        {
            ActivateUnityWindow();
        }

        private void UnityForm_Deactivate(object sender, EventArgs e)
        {
            DeactivateUnityWindow();
        }

        #endregion Event Handlers
    }
}
