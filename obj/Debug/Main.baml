   M S B A M L   `   `   `  ����Q  Mosu!AdvancedDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:osu_AdvancedDownloaderosu_AdvancedDownloader  5   	   '    !osu_AdvancedDownloader.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:osu_AdvancedDownloader     5   	   -   5          �Name$
  Main��5   	    � Title$ osu! downloader��5   	   $	��242��6!   $	��528��6.    � 
ResizeMode$ NoResize=�6:    � WindowStartupLocation$ CenterScreen=�6P   ���	#FF272727$��Disabled=�6�    � Icon$ /osuDownloader.png��6�   .��5	      � .+�5
   
   �� -   5         ��Name$ BeatmapBackground��5
      $��0,0,1,1q�6+   $
��Fill=�6<    J� Effect 5      -  'System.Windows.Media.Effects.BlurEffect  5      5      �� 5   
   -   5         ��Name$% BeatmapDownloadOpenLastBMButton��5      $��OPEN LAST BEATMAP��6:   {�.  $��	0,0,10,66q�6h   $��Right=�6{   $	��249��6�   $��Bahnschrift)�6�   ���	#594C4C4C���	#FF929292
�������$��15(�6�   $��20��6  $��Bottom=�6   � Cursor$
 Hand��63   5        5      ��5      �� 5      	 �� 
TargetType"	 ��6   .�5      �� 
 �� Property
 � �� �� Value �	#FF4C4C4C63   �� 5      
 � �� 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� #��z��6)   ���	#FF929292$��1q�6k   ._�5   &   �� $��Center=�67   $��Center=�6T   5   #   5      5       �� Triggers 5      P� 5        P� Property �@J�IsMouseOver P� Value . . �5!      �� 
 � ��
 �  ��6;   5"      5$      5&      �� 5'   
   -   5        $" BeatmapDownloadAndOpenButton��5'      $��DOWNLOAD AND OPEN��67   $��
10,0,10,38q�6S   $��Bahnschrift)�6g   ���	#FF929292���	#594C4C4C
�������$��15(�6�   $��23��6�   $��Bottom=�6�   $
 Hand��6�   ��5(      �� 5)      "	 ��6   .�5*      �� 
 � �� �	#FF4C4C4C63   �� 5+      
 � �� 5,      �� 5-      " ��6.   .R�5.   "   �� #��z��6)   ���	#FF929292$��1q�6k   ._�5/   &   �� $��Center=�67   $��Center=�6T   50   #   51      53       54      P� 55       �@J�IsMouseOver . . �56      �� 
 � ��
 �  ��6;   57      59      5;      �� 5<   
   -   5        $ BeatmapDownloadButton��5<      $��DOWNLOAD��60   $��10,0,264,66q�6C   $��Bahnschrift)�6X   ���	#594C4C4C���	#FF929292
�������$��15(�6�   $��20��6�   $��Bottom=�6�   $��1,1,1,1q�6�   $
 Hand��6  ��5=      �� 5>      "	 ��6   .�5?      �� 
 � �� �	#FF4C4C4C63   �� 5@      
 � �� 5A      �� 5B      " ��6.   .R�5C   "   �� #��z��6)   ���	#FF929292$��1q�6k   ._�5D   &   �� $��Center=�67   $��Center=�6T   5E   #   5F      5H       5I      P� 5J       �@J�IsMouseOver . . �5K      �� 
 � ��
 �  ��6;   5L      5N      5P      � 5Q   
   -   5         �Name$ BeatmapProgressBar��5Q      $��
10,0,10,10q�62   ���	#594C4C4C���	#FF929292���	#FF26B540$��23��6�   $��Bottom=�6�   �� 5R   
   -   5         ��Name$ BeatmapBoxLink��5R      $��
10,10,10,0q�6*   $�� ��6>   $��27��6F   $	��Top=�6R   $��Bahnschrift)�6j   ���	#FF929292���	#594C4C4C
������� �� SelectionBrush �	#FF2E91E0$��15(�6�    �� MaxLines$ 1��6�    �� 	MaxLength$ 60��6�   �� 5S   
   ���	#FF929292$��1q�6)    �� CornerRadius$ 1��6=   $��10,42,10,91q�6N   ._�5T      �� -   5         ��Name$ BeatmapInformationText��5T      $
��Wrap=�68   $�� ��6L   $��Bahnschrift)�6T   
����������	#594C4C4C$��15(�6�    �� Padding$ 2,2,2,2q�6�   5U      5V      5W      