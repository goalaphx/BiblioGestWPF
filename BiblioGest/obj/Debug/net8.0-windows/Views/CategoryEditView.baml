   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      '    !BiblioGest.Views.CategoryEditView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value ��Name �� ElementName �� Path �� Triggers P� Property	 P� Value��%  '��       '�� �    �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,8q�6'   �� 5       � ��$ 2q�6(   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      �� .+�5      �� $ controlPlaceholder��68   �� 5      
���  ��$��10(�6�   $��0,1,0,0q�6�   ��6+   �� $ controlPlaceholder��$( "AdornedElement.(Validation.Errors)���5   "   �� 5   &   .R�5   *   �� ��64   �� *ErrorContent+5   '   5      5      5      5!       5"      P� 5#       �@=�HasError$
	 true��69   . �5$      ��  � ��
 �  ��64   �� 5%       � ��$ 1q�68   5&      5(      �� 5)   
   " ��6   .�5*      ��  � ��$ Center=�62   �� 5+       � ��$ 0,0,10,8q�6'   5,      5-      .��5/      � $��20q�6   
 � RowDefinitions
 50   
   �� 51      $
��Auto �6   �� 52      $��* �6   �� 53      $
��Auto �6   .+�56   
   �� $��0��6   $��20(�6>   $��SemiBold"�6L   $��0,0,0,15q�6b   $��Center=�6t   ��6!   �� *WindowTitle+6t   �� 58   
   $��1��6   $
��Auto=�6$   .��59      �  � ColumnDefinitions 5:      �� 5;      $
��Auto �6'   $	��100��64   �� 5<      $��* �6'   
 5>      �� 5?      $
��Auto �6$   �� 5@      $
��Auto �6$   .+�5C      �� $��0��6   $��0��6%   $��_Nom:��65    �� Target 6E   �� $ txtNom���� 5D      -   5         ��Name$ txtNom��5D   7   $��0��6   $��1��6'    �� 	MaxLength$	 100��5F      ��5E      �� *Nom+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��5F      �� 5H      $��1��6   $��0��6%   $��_Description:��65   $	��Top=�6{   $��0,3,10,8q�6�    6M   �� $ txtDescription��6�   �� 5I      -   5        $ txtDescription��5I   7   $��1��6   $��1��6'   $	 500��5K       �� TextWrapping$
 Wrap=�6*    �� AcceptsReturn$
 True��6>   $��80��6S   " �� VerticalScrollBarVisibility$
 Auto=�6b   ��5J      �� *Description+$ TwoWay=�$ PropertyChanged=�$
 True��5K   b   5L      5M      �� 5O   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�6W   .+�5P      �� $��_Sauvegarder��6    �� 	IsDefault$
 True��6t   $	��100��6�   $��0,0,10,0q�6�   $
��10,5q�6�   ��6,   �� *SaveCategoryCommand+{�6T   �� *	IsNotBusy+6�   �� 5Q      $��A_nnuler��6    �� IsCancel$
 True��6n   $	��100��6~   $
��10,5q�6�   ��6(   �� *CancelEditCommand+{�6N   �� *	IsNotBusy+6�   5R      � 5U   
   $��0��6   $��3��6   ���	#60FFFFFF K� ZIndex$ 10��6D   y�5V      �� *IsBusy+    BooleanToVisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.+�5W      �� 
���OO/� �� CornerRadius$ 5��60    �� Padding$ 20q�6A    J� Opacity$	 0.8Q�6N   $��Center=�5X      $��Center=�62   ._�5Y      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5Z      5[      5\      5]      