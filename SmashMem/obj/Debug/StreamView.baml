   M S B A M L   `   `   `  ����C  ?SmashMem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SmashMemSmashMem  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5          SmashMem.StreamView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:SmashMem     5      6sys&clr-namespace:System;assembly=mscorlib     5      P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5         RadioButtonListItem �� 
TargetType �� Property �� Value �� 
TargetType �� 
IsSelected d� 	IsChecked J� IsHitTestVisible	 �� ContentStringFormat
 �� ContentStringFormat J� SnapsToDevicePixels  RadioButtonList �� 	Resources �� Triggers P� Property P� Value  HorizontalRadioButtonList �� BasedOn $� Orientation  SpacedRadioButtonListItem��%\  &	        &	 �    7   &	 <    7  &	 �    7   �� 5      " ��6&   .�5      ��  � �� 5      �� 5      " ��6   .R�5      ]� ��. #��z��6'   $��Stretch=�6Q   $��Center=�6o   .+�5   	   	� # z 6   |�.   .  $��Center=�6i   $��0,0,4,0q�6�   �� 5   	   #��z��5      #��z��5      #
 z	 5      #��z��5      #��z��5      # z 5      5    	   5"      5&      5(      �� 5*      " ��6"    5,      ���� 5-      '��       " ��6   .�5.      ��  � ��$ 0q�6!   5/      50      .�52      ��  � ��$ 0q�6'   �� 53       � ��
 ���� 6'   �� 55       � ��8   6*   �� 57       � �� 58      �� 59      " ��6   .R�5:      �� # z 6   5;      5=       5?      P� 5@       � {� .  . �5A      ��  � ��# � ��6.   5B      5E      �� 5G      8   " ��6W   .�5H      ��  � �� 5I      �� 5J      .R�5K      $� 
������ $ 
Horizontal=�68   5L      5N      5O      �� 5P      " ��6,   8   .�5Q      ��  � ��$	 0,2q�6   5R      5S      .��5T      (� .��5U      � .+�5V      �� $
��Left=�6Y   $	��Top=�6t   $	��495��6�   $��
10,10,10,0q�6�   $��20��6�   ��6   �� *SetOutputCommand+��61   �� *SetOutputButtonText+6�   �� 5W      $��Update��6.   $
��Left=�6?   $	��Top=�6Z   $	��200��6r   $��	10,35,0,0q�6~   ��6   �� *UpdateCommand+6~   �� 5X      $��Reset Scores��62   $
��Left=�6I   $	��Top=�6d   $��85��6|   $��
215,35,0,0q�6�   ��6   �� *ResetScoreCommand+6�   �� 5Y      $
��Swap��6,   $
��Left=�6;   $	��Top=�6V   $	��200��6n   $��
305,35,0,0q�6z   ��6   �� *SwapCommand+6z   �� 5[      $
��Left=�6   $��23��6(   $��	10,60,0,0q�64    �� TextWrapping$
 Wrap=�6G   $	��Top=�6�   $	��245��6�   ��6[   �� *	Header1+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5\      $
��Left=�6   $��23��6(   $��
260,60,0,0q�64   $
 Wrap=�6H   $	��Top=�6�   $	��245��6�   ��6\   �� *	Header2+$ PropertyChanged=�6�   �� 5^      $
��Left=�6   $��23��6(   $
 Wrap=�64   $	��Top=�6�   $	��200��6�   $��	10,88,0,0q�6�   ��6H   �� *Player1Name+$ PropertyChanged=�6�   �� 5_      $
��Left=�6   $��23��6(   $
 Wrap=�64   $	��Top=�6�   $	��200��6�   $��
305,88,0,0q�6�   ��6H   �� *Player2Name+$ PropertyChanged=�6�   �� 5`      $
��Left=�6   $��23��6(   $
 Wrap=�64   $	��Top=�6�   $��40��6�   $��
215,88,0,0q�6�   ��6H   �� *Player1Score+$ PropertyChanged=�6�   �� 5a      $
��Left=�6   $��23��6(   $
 Wrap=�64   $	��Top=�6�   $��40��6�   $��
260,88,0,0q�6�   ��6H   �� *Player2Score+$ PropertyChanged=�6�   �� 5b      $	��▲��6=   $��6(�6I   $��-5q�6V   $
��Left=�6c   $	��Top=�6~   $��18��6�   $��
235,90,0,0q�6�   $��9��6�   ��6   �� *IncrementPlayer1ScoreCommand+6�   �� 5c      $	��▼��6=   $��6(�6I   $��-5q�6V   $
��Left=�6c   $	��Top=�6~   $��18��6�   $��235,100,0,0q�6�   $��9��6�   ��6   �� *DecrementPlayer1ScoreCommand+6�   �� 5d      $	��▲��6=   $��6(�6I   $��-5q�6V   $
��Left=�6c   $	��Top=�6~   $��18��6�   $��
280,90,0,0q�6�   $��9��6�   ��6   �� *IncrementPlayer2ScoreCommand+6�   �� 5e      $	��▼��6=   $��6(�6I   $��-5q�6V   $
��Left=�6c   $	��Top=�6~   $��18��6�   $��280,100,0,0q�6�   $��9��6�   ��6   �� *DecrementPlayer2ScoreCommand+6�   �� 5g      $
��Left=�6   $��23��6(   $��215,116,0,0q�64   $
 Wrap=�6I   $	��Top=�6�   $��85��6�   ��6]   �� *GameCountLimit+$ PropertyChanged=�6�   �� 5h      $
��Left=�6   $��23��6(   $��
10,196,0,0q�64   $
 Wrap=�6H   $	��Top=�6�   $	��200��6�   ��6\   �� *Player1Character+$ PropertyChanged=�6�   �� 5i      $
��Left=�6   $��23��6(   $��305,196,0,0q�64   $
 Wrap=�6I   $	��Top=�6�   $	��200��6�   ��6]   �� *Player2Character+$ PropertyChanged=�6�   �� 5j      $��Get character selection��6   $
��Left=�6�   $��
10,224,0,0q�6�   $	��Top=�6�    69   �� *GetCharacters+$ PropertyChanged=�6�   �� 5k      $!��Increment score on game win��6   $
��Left=�6�   $��
10,244,0,0q�6�   $	��Top=�6�    6=   �� *IncrementScore+$ PropertyChanged=�6�   �� 5l      $#��Decrement score on stock loss��6   $
��Left=�6�   $��
10,264,0,0q�6�   $	��Top=�6�    6?   �� *DecrementScore+$ PropertyChanged=�6�   �� 5m      $��Fire hotkey on game start��6   $
��Left=�6�   $��
10,284,0,0q�6�   $	��Top=�6�    6;   �� *FireGameStartHotkey+$ PropertyChanged=�6�   �� 5n      $��Fire hotkey on match end��6   $
��Left=�6�   $��
10,304,0,0q�6�   $	��Top=�6�    6:   �� *FireGameEndHotkey+$ PropertyChanged=�6�   �� 5o      #��[ 63   #��[ 6\   $
��Left=�6�   $	��Top=�6�   $��
10,113,0,0q�6   �� SelectedValue 6   �� *Player1Port+��6�   ��  �� Source )   #SmashMem.EnumBindingSourceExtension  *M� *
local:Port++6  �� 5p      #��[ 63   #��[ 6\   $
��Left=�6�   $	��Top=�6�   $��305,113,0,0q�6   6   �� *Player2Port+��6�   ��    *M� *
local:Port++6  �� 5q      $
��Left=�6}   $��70��6�   $	��Top=�6�   $	��200��6�   $��305,224,0,0q�6�    �� SelectedItem 6   �� *SelectedProcess+$ PropertyChanged=���6[   �� *	Processes+6�   �� 5r      $��Get Processes��64   $
��Left=�6L   $	��Top=�6g   $	��200��6   $��305,299,0,10q�6�   ��6   �� *GetProcessesCommand+6�   5t      5u      5v      