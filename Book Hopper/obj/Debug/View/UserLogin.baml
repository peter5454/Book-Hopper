   M S B A M L   `   `   `  ����F  BBook Hopper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BookHopperApp.ViewBookHopperApp.View  5   	   ?$clr-namespace:Book_Hopper.ViewModelsBook_Hopper.ViewModels  5   	   G(clr-namespace:Book_Hopper.CustomControlsBook_Hopper.CustomControls  5   	   "    BookHopperApp.View.UserLogin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:BookHopperApp.View     5   	   :	ViewModel$clr-namespace:Book_Hopper.ViewModels     5   	   Ccustomcontrols(clr-namespace:Book_Hopper.CustomControls     5   	     � Title$  	UserLogin��5
   	   $
��1080��6   $
��1920��6)    � WindowState$ 	Maximized=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      /  )Book_Hopper.ViewModels.UserLoginViewModel  5   
   .��5      �� ���#4AC7EF._�5   
   � .+�5      �  � RowDefinitions 5      �� 5      $��83 �6    6.   .+�5      � ���#B6C197 � ColumnDefinitions 5      �� 5      �� 5       $
��1059 �6+   �� 5!      $��49 �6+   �� 5"      $��90 �6+   �� 5#      $��33 �6+   �� 5$      $��90 �6+   �� 5%      $��33 �6+   �� 5&      $��90 �6+   �� 5'      $��27 �6+   .+�5)      �� $��0��6   
������ $��0q�6F   .��5*      �� $��/Images/LOGOwithname.png��6    $	��360��5+      $��70��6$   $��0��5,      $
��Left=�5-      $��27,0,0,0q�5.      5/      51      52      � 53      $��0,83,0,0q�6   .+�54      �� $��/Images/Lilies.png��6   55      �� 57      $	��639��6    �� Orientation$ Vertical=�6%   $��640,136,640,446q�6<   ���#B6C197	 � 	Resources	 58      
 �� 
TargetType �� Property �� Value��%`   '��       �� 59      "
 ��6   .�5:      ��  �@��CornerRadius$	 100��69   5;      5<      .+�5=      �� $��/Images/LOGOwithname.png��6   $
��0,20q�6:   $	��360��6H   �� 5>      $��Username��6   ���#000000$��24(�5@      $
��Bold"�5A      $��Center=�5B      �� 5D      -   5         ��Name$ UsernameTextBox��5D      $	��468��6r   $��62��6~   $��Center=�6�   $��18(�6�   $��20,0,0,0q�6�   ��63   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�.��6�   5E      �� 5F      $��Password��6   ���#000000$��24(�5H      $
��Bold"�5I      $��Center=�5J      $��0,30,0,0q�5K      4   .Book_Hopper.CustomControls.BindablePasswordBox  5N      -   5          Name$ PasswordTextBox��5N   9   
�������$	��468��6�   $��62��6�   $��Center=�6�   $��18(�6�   $��20,0,0,0q�6�      Password 6R   �� *
Password+ �� Mode$ TwoWay=�$ PropertyChanged=�6�   �� 5O      ���#931F1D$��Center=�6O   $��15(�6j   $��PT Sans Narrow)�6x   $
��Wrap=�6�   $��30��6�   ��6   �� *ErrorMessage+6�   �� 5P      -   5         ��Name$ Login��5P      $	��222��6(   $��62��64   $��0,0,0,0q�6@   ���#934D40��6f   �� *LoginCommand+.��5Q      �� ���#D5DDBC$��24(�65   $
��Bold"�6C   Login6\   5R      �� 5S      -   5        $ Register��5S      $	��222��6+   $��22��67   ���#8A9B68���#D5DDBC$��12(�6m   $
��Bold"�6{   $��208,10,209,0q�6�   ��6�   �� *RegisterCommand+.��5T      �� ���#D5DDBC$��12(�65   $
��Bold"�6C   
Register6_   5U      5V      5X      5Z      5[      