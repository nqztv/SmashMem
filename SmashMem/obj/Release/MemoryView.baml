   M S B A M L   `   `   `  ���� C  ?SmashMem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SmashMemSmashMem      SmashMem.MemoryView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     (localclr-namespace:SmashMem     P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .��� .+��� $��90,27,10,10q����� *
InputCSV+�� $��Gecko Connected��$
��Left=�$	��Top=�$��	10,10,0,0q����� *ToggleConnectionCommand+ d� 	IsChecked �� *GeckoConnected+ �� UpdateSourceTrigger$ PropertyChanged=��� $��	Set Input��$
��Left=�$	��Top=�$��75��$��	10,30,0,0q����� *SetInputCommand+�� $
��Peek��$
��Left=�$	��Top=�$��75��$��	10,55,0,0q����� *PeekCommand+�� $
��Poke��$
��Left=�$	��Top=�$��75��$��	90,55,0,0q����� *PokeCommand+�� $��	Auto Peek��$
��Left=�$	��Top=�$��
170,58,0,0q����� *ToggleAutoPeekCommand+ �� *
AutoPeek+$ PropertyChanged=��� $
��Left=�$��23�� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��$��
249,56,0,0q����� *
Interval+$ PropertyChanged=�&   System.Windows.Controls.DataGrid  $
��Left=�$	��Top=�$��	10,84,0,0q����� *MemoryAddresses+