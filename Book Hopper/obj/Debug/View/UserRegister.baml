   M S B A M L   `   `   `  ����F  BBook Hopper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BookHopperApp.ViewBookHopperApp.View  5   	   ?$clr-namespace:Book_Hopper.ViewModelsBook_Hopper.ViewModels  5   	   G(clr-namespace:Book_Hopper.CustomControlsBook_Hopper.CustomControls  5   	   %    BookHopperApp.View.UserRegister   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:BookHopperApp.View     5   	   :	ViewModel$clr-namespace:Book_Hopper.ViewModels     5   	   Ccustomcontrols(clr-namespace:Book_Hopper.CustomControls     5   	     � Title$  UserRegister��5
   	   $
��1080��6   $
��1920��6,    � WindowState$ 	Maximized=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      2  ,Book_Hopper.ViewModels.UserRegisterViewModel  5   
   .��5      �� ���#4AC7EF._�5   
   � .+�5      �  � RowDefinitions 5      �� 5      $��83 �6$   62   .+�5      � ���#B6C197 � ColumnDefinitions 5      �� 5      �� 5       $
��1059 �6+   �� 5!      $��49 �6+   �� 5"      $��90 �6+   �� 5#      $��33 �6+   �� 5$      $��90 �6+   �� 5%      $��33 �6+   �� 5&      $��90 �6+   �� 5'      $��27 �6+   .+�5*      �� $��/Images/LOGOwithname.png��6   $	��360��5+      $��70��6$   $��0��5,      $
��Left=�5-      $��27,0,0,0q�5.      50      51      � 52      $��0,83,0,0q�6   .+�53      �� $��/Images/Lilies.png��6   54      �� 56      $	��639��6    �� Orientation$ Vertical=�6%   $��640,136,640,221q�6<   ���#B6C197.+�57      �� $��/Images/LOGOwithname.png��6   $
��0,20q�6:   $	��360��6H   � 58       59      �� 5:      �� 5;      .+�5=      �� $ Vertical=�6!   $��Center=�68   $��0��6U   .+�5>      �� $��
First Name��6$   ���#000000$��24(�5@      $
��Bold"�5A      $��Center=�5B      �� 5D      $	��280��6"   $��62��6.   $��Center=�6:   $��18(�6\   $��20,0,0,0q�6j   ��6}   �� *	FirstName+	 �� UpdateSourceTrigger$	 PropertyChanged=�5E      �� 5F      $ Vertical=�6!   $��Center=�68   $��1��6U   .+�5G      �� $��	Last Name��6$   ���#000000$��24(�5I      $
��Bold"�5J      $��Center=�5K      �� 5M      $	��280��6"   $��62��6.   $��Center=�6:   $��18(�6\   $��20,0,0,0q�6j   ��6}   �� *
LastName+$	 PropertyChanged=�5N      5O      �� 5P      $��Username��6   ���#000000$��24(�6A   $
��Bold"�6O   $��Center=�6a   $��0,30,0,0q�6~   �� 5Q      $	��468��6   $��62��6&   $��Center=�62   $��18(�6T   $��20,0,0,0q�6b   ��6u   �� *
Username+$	 PropertyChanged=��� 5S      $��Email��6   ���#000000$��24(�6>   $
��Bold"�6L   $��Center=�6^   $��0,30,0,0q�6{   �� 5T      $	��468��6   $��62��6&   $��Center=�62   $��18(�6T   $��20,0,0,0q�6b   ��6u   �� *Email+$	 PropertyChanged=�� 5V      $
��0,20q�6    5W      �� 5X      �� 5Y      .+�5[      �� $ Vertical=�6!   $��Center=�68   $��0��6U   .+�5\      �� $��Password��6$   ���#000000$��24(�5^      $
��Bold"�5_      $��Center=�5`      4   .Book_Hopper.CustomControls.BindablePasswordBox  5b      $	��280��6=   $��62��6I   $��Center=�6U   $��18(�6w   $��20,0,0,0q�6�   
�������
   Password
 6�   �� *
Password+ �� Mode$ TwoWay=�$	 PropertyChanged=�5c      �� 5d      $ Vertical=�6!   $��Center=�68   $��1��6U   .+�5e      �� $��Confirm Password��6$   ���#000000$��24(�5g      $
��Bold"�5h      $��Center=�5i        5k      $	��280��6=   $��62��6I   $��Center=�6U   $��18(�6w   $��20,0,0,0q�6�   
�������
 6�   �� *ConfirmPassword+$ TwoWay=�$	 PropertyChanged=�5l      5m      �� 5n      ���#931F1D$��Center=�6O   $��12(�6j   $��PT Sans Narrow)�6x   $��0,0,0,0q�6�   $
��Wrap=�6�   $��30��6�   ��6   �� *ErrorMessage+6�   �� 5o      -   5         ��Name$ Register��5o      $	��222��6+   $��62��67   ���#934D40��6X   �� *RegisterCommand+.��5p      �� ���#D5DDBC$��24(�65   $
��Bold"�6C   Create Account6e   5q      �� 5r      -   5        $ Login��5r      $	��222��6(   $��22��64   ���#8A9B68���#D5DDBC$��12(�6j   $
��Bold"�6x   $��208,10,209,0q�6�   ��6�   �� *RedirectToLoginCommand+��6�   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �.��5s      �� ���#D5DDBC$��12(�65   $
��Bold"�6C   Go back to login6g   5t      5u      5w      5y      5z      