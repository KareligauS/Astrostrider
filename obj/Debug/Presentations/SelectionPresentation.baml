   M S B A M L   `   `   `  ����G  CAstrostrider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Astrostrider.PresentationsAstrostrider.Presentations  5      6    0Astrostrider.Presentations.SelectionPresentation   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Astrostrider.Presentations     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      D @ModernWpf, Version=0.9.5.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.5.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5
      -   5        $��ThisControl��5      $
��1080��5      $
��1920��6   .��5      �   � DataContext  6   ��  �� ElementName$ ThisControl�� � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��2* �6   6*   �� 5      $��7* �6   6*   �� 5      $��6* �6   6*   �� 5      $��2* �6   6*   .+�5   
   �� $��2��6   $��4��6$   $n��hC:\Users\user\Desktop\Astrostrider\Astrostrider\Resources\Pictures/SelectionPresentationBackgroundHD.png��65   $
��Fill=�6�   6�   �� 5   
   $��0��6   $��0��6!   $��Center=�6.   ���#4B4B4B$��32(�6`   ��5      �� *!SpaceShipSelectionButtonContent+��5      �� *SpaceShipSelectCommand+�� 5   
   $��0��6   $��1��6    $	��280��6-   $	��280��6:   ��5      �� *SpaceShipImageSource+�� 5    
   $��0��6   $��2��6$    �� Padding$ 0,30,0,0q�61   $
��Wrap=�6D    �� TextAlignment$ Center=�6X   ���#4B4B4B$��20(�6�   ��5!      �� *SpaceShipDescriptionText+�� 5#   
   $��1��6   $��0��6!   $��Center=�6.   ���#F2F8FD$��32(�6`   ��5$      �� *#!SpaceObjectSelectionButtonContent+��5%      �� *SpaceObjectSelectCommand+�� 5&   
   $��1��6   $��1��6    $	��280��6-   $	��280��6:   ��5'      �� *SpaceObjectImageSource+�� 5(   
   $��1��6   $��2��6$   $ 0,30,0,0q�61   $
��Wrap=�6D   $ Center=�6X   ���#F2F8FD$��20(�6�   ��5)      �� *SpaceObjectDescriptionText+�� 5+   
   $��2��6   $��3��6%   $��Center=�62   
���   �
�������$��32(�6u   $��Поехали��5,      {�5-      �� *IsSwitchButtonEnabled+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5.      +  %Microsoft.Xaml.Behaviors.EventTrigger  5/         	EventName$ Click��6!   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 50      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  	   Command	 6,   �� *SwitchPresentationCommand+51      53      54      55      