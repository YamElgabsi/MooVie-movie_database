   M S B A M L   `   `   `  ����G  CMovieReviews, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MovieReviewsMovieReviews  5   	       MovieReviews.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MovieReviews     5   	   -   5          � Title$  Login��5	   	   $	��550��6   $	��800��6$    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency$
 True��5   	   .��5      ��  �� CornerRadius$ 15��6   ��5   
   �� 5       �� ImageSource$ /Images/Login.jpg��6   6;   ._�5   
   �� $ 12��5       J� Opacity$	 0.7Q�5      $��3q�5      ��5      �� 5       �� 
StartPoint$	 0,0&�6&   	 �� EndPoint$		 1,1&�67   .��5      � 
 � Color$
 #462AD8��6#    � Offset$ 0Q�63   6@   � 5      $
 #DA34EA��6#   $
 0.75Q�63   6C   � 5      $
 #8A16C1��6#   $ 1Q�63   6@   5      ��5!      �� 5"      $	 0,1&�6&   $		 1,0&�67   .��5#      � $
 #060531��6#   $ 0Q�63   6@   � 5$      $
 #1B1448��6#   $
 0.75Q�63   6C   5%      ._�5(      �  � RowDefinitions 5)      �� 5*      $��30 �6$   62   �� 5+      6&   .+�5.      �  � ColumnDefinitions 5/      �� 50      6-   �� 51      $��25 �6+   68   �� 52      $��25 �6+   68   �� 53      $��5 �6+   67   .+�56      �� -   5        $��textblock_name��57       $��MooVie��56       
�������$��10(�59       $��
Montserrat)�5:       $��Center=�5;       $��10,0,0,0q�5<       64   �� 5>      -   5        $��btn_Minimize��5>      $��0q�5?      $��-��5@       � Cursor$
 Hand��5A      
�������$��16(�5C      $��
Montserrat)�5D      $��1��5E      ��5G      �� 5H       �� 
TargetType" ��6$   .�5I   "   ��  �� Property � �� �� Value �#28AEED6Q    �� Triggers 5J   "   P� 5K   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5L   *   ��  � �� �#278BEF6Y   5M   '   5O      ��5Q      �� 5R       �� 
TargetType" ��6.   .R�5S   "   �� $��18��6)   $��18��64   $ 9��6@   #��z��5T   )   ._�5U   &   �� $��Center=�67   $��Center=�5V   7   6V   5W   #   5Y      5\      �� 5^      -   5        $��	btn_Close��5^      $��0q�5_      $��x��5`      $
 Hand��5a      
�������$��16(�5c      $��
Montserrat)�5d      $��2��5e      ��5g      �� 5h      " ��6$   .�5i   "   ��  � �� �#DA34AE6Q    5j   "   P� 5k   &    �@J�IsMouseOver$
 True��6E   . �5l   *   ��  � �� �#C62DAE6Y   5m   '   5o      ��5q      �� 5r      " ��6.   .R�5s   "   �� $��18��6)   $��18��64   $ 9��6@   #��z��5t   )   ._�5u   &   �� $��Center=�67   $��Center=�5v   7   6V   5w   #   5y      5{      5|      �� 5~      $	��250��6   $��2��5       �� Orientation$ Vertical=�5�      $��0,35,0,0q�5�      .+�5�      �� -   5        $��btn_profile��5�      $��0q�5�      $��	MyProfile��5�      $
 Hand��5�      
�������$��12(�5�      $��
Montserrat)�5�      $��2��5�      $��0,30,0,0q�5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#462AD86Q    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �#28AEED6Y   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $ 20��6A   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   6V   5�   #   5�      5�      �� 5�      -   5        $��
btn_movies��5�      $��0q�5�      $��Movies��5�      $
 Hand��5�      
�������$��12(�5�      $��
Montserrat)�5�      $��2��5�      $��0,30,0,0q�5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#462AD86Q    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �#28AEED6Y   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $ 20��6A   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   6V   5�   #   5�      5�      �� 5�      -   5        $��	btn_stars��5�      $��0q�5�      $��Stars��5�      $
 Hand��5�      
�������$��12(�5�      $��
Montserrat)�5�      $��2��5�      $��0,30,0,0q�5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#462AD86Q    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �#28AEED6Y   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $ 20��6A   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   6V   5�   #   5�      5�      �� 5�      -   5        $��btn_companies��5�      $��0q�5�      $��	Companies��5�      $
 Hand��5�      
�������$��12(�5�      $��
Montserrat)�5�      $��2��5�      $��0,30,0,0q�5�      ��5�      �� 5�      " ��6$   .�5�   "   ��  � �� �#462AD86Q    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �#28AEED6Y   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $ 20��6A   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   6V   5�   #   5�      5�      �� 5     -	   5        $��	btn_users��5     $��0q�5     $��Users��5     $
 Hand��5     
�������$��12(�5     $��
Montserrat)�5	     $��2��5
     $��0,30,0,0q�5     ��5     �� 5     " ��6$   .�5  "   ��  � �� �#462AD86Q    5  "   P� 5  &    �@J�IsMouseOver$
 True��6E   . �5  *   ��  � �� �#28AEED6Y   5  '   5     ��5     �� 5     " ��6.   .R�5  "   �� $	��150��6)   $��40��65   $ 20��6A   #��z��5  )   ._�5  &   �� $��Center=�67   $��Center=�5  7   6V   5  #   5     5!     5$     5'     5*     5+     5,     