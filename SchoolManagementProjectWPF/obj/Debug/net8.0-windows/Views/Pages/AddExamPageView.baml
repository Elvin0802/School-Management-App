   M S B A M L   `   `   `  ����U  QSchoolManagementProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:SchoolManagementProjectWPF.Views.Pages&SchoolManagementProjectWPF.Views.Pages  5      <    6SchoolManagementProjectWPF.Views.Pages.AddExamPageView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:SchoolManagementProjectWPF.Views.Pages     5      -   5        $��AddExamPage��5	      $
��auto��5      $
��auto��6   .��5      �� $��Center=�6   $��Center=�6,   .+�5   
   ��   �� Orientation$  
Horizontal=�6   $��Center=�6.   $��Center=�6K   .+�5      �� $	��200��6   $��24��6$   $��Select Subject��60   $
��10,5q�6F   $��Center=�6T   $��Center=�6o    
  BaMode#���   5       
 FrMode#���  68   �� 5      -   5         ��Name$ SubjectsCBox��5      $	��200��6-   $
��10,5q�69   #���   5      #���  6:    � ToolTip$ Subjects��6`    �� SelectedIndex 5   G   �� *SubjectIndex+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5      5      �� 5   
   $  
Horizontal=�6   $��Center=�6.   $��Center=�6K   .+�5      �� $	��200��6   $��24��6$   $��Question Count��60   $
��10,5q�6F   $��Center=�6T   $��Center=�6o   #���   5      #���  68   �� 5      $	��200��6   $
��10,5q�6k   #���   5      #���  68   $ Question Count��6^   ��5   "   �� *QCount+$ PropertyChanged=�$ TwoWay=�5   ^   5       �� 5"   
   $��5��6   $  
Horizontal=�6"   $��Center=�6;   $��Center=�6X   $��0,30,0,0q�6s   .+�5$      �� $
��Save��6   $��0,0,20,0q�6$   #���  66   #���   6\   ��5%      �� *SaveExamCommand+��67   ��  �� ElementName$ AddExamPage���� 5'      $��Cancel��6   $��	20,0,20,0q�6&   #���  69   #���   6_   ��5(      �� *BackCommand+��62   �� $ AddExamPage���� 5*      $��Change Theme��6   $	��180��6,   #���  6_   $��20,0,0,0q�5+      #���   6&   ��5*   8   �� *ChangeThemeCommand+5+   &   5-      5/      50      