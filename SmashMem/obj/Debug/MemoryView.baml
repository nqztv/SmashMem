   M S B A M L   `   `   `  ����C  ?SmashMem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SmashMemSmashMem  5          SmashMem.MemoryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:SmashMem     5      P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .��5      � .+�5      �� $��90,27,10,10q�6'   ��6
   �� *
InputCSV+6'   �� 5      $��Gecko Connected��6   $
��Left=�6�   $	��Top=�6�   $��	10,10,0,0q�6�   ��6(   �� *ToggleConnectionCommand+ d� 	IsChecked 6T   �� *GeckoConnected+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      $��	Set Input��6   $
��Left=�6C   $	��Top=�6^   $��75��6v   $��	10,30,0,0q�6�   ��6   �� *SetInputCommand+6�   �� 5      $
��Peek��6   $
��Left=�6:   $	��Top=�6U   $��75��6m   $��	10,55,0,0q�6x   ��6   �� *PeekCommand+6x   �� 5      $
��Poke��6   $
��Left=�6:   $	��Top=�6U   $��75��6m   $��	90,55,0,0q�6x   ��6   �� *PokeCommand+6x   �� 5      $��	Auto Peek��6   $
��Left=�6�   $	��Top=�6�   $��
170,58,0,0q�6�   ��6!   �� *ToggleAutoPeekCommand+ 6K   �� *
AutoPeek+$ PropertyChanged=�6�   �� 5      $
��Left=�6   $��23��6'    �� TextWrapping$
 Wrap=�63   $	��Top=�6�   $	��120��6�   $��
249,56,0,0q�6�   ��6G   �� *
Interval+$ PropertyChanged=�6�   &   System.Windows.Controls.DataGrid  5      $
��Left=�65   $	��Top=�6P   $��	10,84,0,0q�6h   ��6   �� *MemoryAddresses+6h   5      5      