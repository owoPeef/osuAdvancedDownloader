   M S B A M L   `   `   `  ���� Q  Mosu!AdvancedDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:osu_AdvancedDownloaderosu_AdvancedDownloader  '    !osu_AdvancedDownloader.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:osu_AdvancedDownloader     -     �Name$
  Main�� � Title$ osu! downloader��$	��242��$	��528�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=����	#FF272727$��Disabled=� � Icon$ /osuDownloader.png��.��� .+��� -    ��Name$ BeatmapBackground��$��0,0,1,1q�$
��Fill=� J� Effect -  'System.Windows.Media.Effects.BlurEffect  �� -    ��Name$% BeatmapDownloadOpenLastBMButton��$��OPEN LAST BEATMAP��{�.  $��	0,0,10,66q�$��Right=�$	��249��$��Bahnschrift)����	#594C4C4C���	#FF929292
�������$��15(�$��20��$��Bottom=� � Cursor$
 Hand��   ���� 	 �� 
TargetType"	 ��.��� 
 �� Property
 � �� �� Value �	#FF4C4C4C�� 
 � �� ��  �� 
TargetType" ��.R��� #��z�����	#FF929292$��1q�._��� $��Center=�$��Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ��� 
 � ��
 �  ���� -   $" BeatmapDownloadAndOpenButton��$��DOWNLOAD AND OPEN��$��
10,0,10,38q�$��Bahnschrift)����	#FF929292���	#594C4C4C
�������$��15(�$��23��$��Bottom=�$
 Hand������ "	 ��.��� 
 � �� �	#FF4C4C4C�� 
 � �� �� " ��.R��� #��z�����	#FF929292$��1q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� 
 � ��
 �  ���� -   $ BeatmapDownloadButton��$��DOWNLOAD��$��10,0,264,66q�$��Bahnschrift)����	#594C4C4C���	#FF929292
�������$��15(�$��20��$��Bottom=�$��1,1,1,1q�$
 Hand������ "	 ��.��� 
 � �� �	#FF4C4C4C�� 
 � �� �� " ��.R��� #��z�����	#FF929292$��1q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� 
 � ��
 �  ��� -    �Name$ BeatmapProgressBar��$��
10,0,10,10q����	#594C4C4C���	#FF929292���	#FF26B540$��23��$��Bottom=��� -    ��Name$ BeatmapBoxLink��$��
10,10,10,0q�$�� ��$��27��$	��Top=�$��Bahnschrift)����	#FF929292���	#594C4C4C
������� �� SelectionBrush �	#FF2E91E0$��15(� �� MaxLines$ 1�� �� 	MaxLength$ 60���� ���	#FF929292$��1q� �� CornerRadius$ 1��$��10,42,10,91q�._��� -    ��Name$ BeatmapInformationText��$
��Wrap=�$�� ��$��Bahnschrift)�
����������	#594C4C4C$��15(� �� Padding$ 2,2,2,2q�