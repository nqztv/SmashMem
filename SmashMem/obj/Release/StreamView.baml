   M S B A M L   `   `   `  ���� C  ?SmashMem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SmashMemSmashMem  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     SmashMem.StreamView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     (localclr-namespace:SmashMem     6sys&clr-namespace:System;assembly=mscorlib     P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources    RadioButtonListItem �� 
TargetType �� Property �� Value �� 
TargetType �� 
IsSelected d� 	IsChecked J� IsHitTestVisible	 �� ContentStringFormat
 �� ContentStringFormat J� SnapsToDevicePixels  RadioButtonList �� 	Resources �� Triggers P� Property P� Value  HorizontalRadioButtonList �� BasedOn $� Orientation  SpacedRadioButtonListItem��%C  &	        &	 �     7   &	 �    7  &	 �    7   �� " ��.���  � �� �� " ��.R�]� ��. #��z��$��Stretch=�$��Center=�.+�	� # z |�.   .  $��Center=�$��0,0,4,0q��� #��z��#��z��#
 z	 #��z��#��z��# z �� " �� ���� '��       " ��.���  � ��$ 0q�.���  � ��$ 0q���  � ��
 ���� ��  � ��8   ��  � �� �� " ��.R��� # z  P�  � {� .  . ���  � ��# � ���� 8   " ��.���  � �� �� .R�$� 
������ $ 
Horizontal=��� " ��8   .���  � ��$	 0,2q�.��(� .��� .+��� $
��Left=�$	��Top=�$	��495��$��
10,10,10,0q�$��20������ *SetOutputCommand+���� *SetOutputButtonText+�� $��Update��$
��Left=�$	��Top=�$	��200��$��	10,35,0,0q����� *UpdateCommand+�� $��Reset Scores��$
��Left=�$	��Top=�$��85��$��
215,35,0,0q����� *ResetScoreCommand+�� $
��Swap��$
��Left=�$	��Top=�$	��200��$��
305,35,0,0q����� *SwapCommand+�� $
��Left=�$��23��$��	10,60,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��245������ *	Header1+ �� UpdateSourceTrigger$ PropertyChanged=��� $
��Left=�$��23��$��
260,60,0,0q�$
 Wrap=�$	��Top=�$	��245������ *	Header2+$ PropertyChanged=��� $
��Left=�$��23��$
 Wrap=�$	��Top=�$	��200��$��	10,88,0,0q����� *Player1Name+$ PropertyChanged=��� $
��Left=�$��23��$
 Wrap=�$	��Top=�$	��200��$��
305,88,0,0q����� *Player2Name+$ PropertyChanged=��� $
��Left=�$��23��$
 Wrap=�$	��Top=�$��40��$��
215,88,0,0q����� *Player1Score+$ PropertyChanged=��� $
��Left=�$��23��$
 Wrap=�$	��Top=�$��40��$��
260,88,0,0q����� *Player2Score+$ PropertyChanged=��� $	��▲��$��6(�$��-5q�$
��Left=�$	��Top=�$��18��$��
235,90,0,0q�$��9������ *IncrementPlayer1ScoreCommand+�� $	��▼��$��6(�$��-5q�$
��Left=�$	��Top=�$��18��$��235,100,0,0q�$��9������ *DecrementPlayer1ScoreCommand+�� $	��▲��$��6(�$��-5q�$
��Left=�$	��Top=�$��18��$��
280,90,0,0q�$��9������ *IncrementPlayer2ScoreCommand+�� $	��▼��$��6(�$��-5q�$
��Left=�$	��Top=�$��18��$��280,100,0,0q�$��9������ *DecrementPlayer2ScoreCommand+�� $
��Left=�$��23��$��215,116,0,0q�$
 Wrap=�$	��Top=�$��85������ *GameCountLimit+$ PropertyChanged=��� $
��Left=�$��23��$��
10,196,0,0q�$
 Wrap=�$	��Top=�$	��200������ *Player1Character+$ PropertyChanged=��� $
��Left=�$��23��$��305,196,0,0q�$
 Wrap=�$	��Top=�$	��200������ *Player2Character+$ PropertyChanged=��� $��Get character selection��$
��Left=�$��
10,224,0,0q�$	��Top=� �� *GetCharacters+$ PropertyChanged=��� $!��Increment score on game win��$
��Left=�$��
10,244,0,0q�$	��Top=� �� *IncrementScore+$ PropertyChanged=��� $#��Decrement score on stock loss��$
��Left=�$��
10,264,0,0q�$	��Top=� �� *DecrementScore+$ PropertyChanged=��� $��Fire hotkey on game start��$
��Left=�$��
10,284,0,0q�$	��Top=� �� *FireGameStartHotkey+$ PropertyChanged=��� $��Fire hotkey on match end��$
��Left=�$��
10,304,0,0q�$	��Top=� �� *FireGameEndHotkey+$ PropertyChanged=��� #��[ #��[ $
��Left=�$	��Top=�$��
10,113,0,0q� �� SelectedValue �� *Player1Port+����  �� Source )   #SmashMem.EnumBindingSourceExtension  *M� *
local:Port++�� #��[ #��[ $
��Left=�$	��Top=�$��305,113,0,0q� �� *Player2Port+����    *M� *
local:Port++�� $
��Left=�$��70��$	��Top=�$	��200��$��305,224,0,0q� �� SelectedItem �� *SelectedProcess+$ PropertyChanged=����� *	Processes+�� $��Get Processes��$
��Left=�$	��Top=�$	��200��$��305,299,0,10q����� *GetProcessesCommand+