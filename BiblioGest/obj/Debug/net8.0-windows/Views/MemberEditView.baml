   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      %    BiblioGest.Views.MemberEditView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5      ��%    5      .��5      � $��15q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��18(�6>   $��SemiBold"�6L   $��0,0,0,15q�6b   $��Center=�6t   ��6!   �� *WindowTitle+6t   �� 5!   
   $��1��6   $
��Auto=�6$   $��0,0,5,0q�6G   .��5"      ��   5$       �� 
TargetType �� Property �� Value ��Name �� ElementName �� Path �� Triggers	 P� Property
 P� Value��%�  '��       '�� �    '��     �� 5%      " ��6   .�5&      ��  � ��$ 0,0,0,5q�63   �� 5'       � ��$ 2q�64   �� 5(       �@=�ErrorTemplate 5)      �� 5*   "   .R�5+   &   �� .+�5,   *   �� $ controlPlaceholder��6D   �� 5.   *   
���  ��$��10(�6�   $��0,1,0,0q�6�   ��67   �� $ controlPlaceholder��$( "AdornedElement.(Validation.Errors)���5/   .   �� 50   2   .R�51   6   �� ��6@   �� *ErrorContent+52   3   54   +   55   '   56   #   58       59      P� 5:      	 �@=�HasError$

 true��6E   . �5@   "   ��  � ��
 �  ��6@   �� 5A   "    � ��$ 1q�6D   5B      5D      �� 5F      " ��6   .�5H      ��  � ��$ 0,0,0,5q�63   �� 5I       � ��$ 2q�64   �� 5J       �@=�ErrorTemplate 5K      �� 5L   "   .R�5M   &   �� .+�5N   *   �� $ controlPlaceholder��6D   �� 5P   *   
���  ��$��10(�6�   $��0,1,0,0q�6�   ��67   �� $ controlPlaceholder��$( "AdornedElement.(Validation.Errors)���5Q   .   �� 5R   2   .R�5S   6   �� ��6@   �� *ErrorContent+5T   3   5V   +   5W   '   5X   #   5Z       5[      P� 5\      	 �@=�HasError$

 true��6E   . �5a   "   ��  � ��
 �  ��6@   �� 5b   "    � ��$ 1q�6D   5c      5e      �� 5g      " ��6   .�5h      ��  � ��$ Center=�6>   �� 5i       � ��$ 0,0,5,5q�63   5j      5k      .+�5n      �  � ColumnDefinitions 5o      �� 5p      $
��Auto �6+   $	��120��68   �� 5r      $��* �6+    5t      �� 5u      $
��Auto �6(   �� 5w      $
��Auto �6(   �� 5y      $
��Auto �6(   �� 5{      $
��Auto �6(   �� 5}      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6   $��0��6)   $��_Nom:��69    �� Target 6I   �� $ txtNom���� 5�      -   5         ��Name$ txtNom��5�   ;   $��0��6   $��1��6+    �� 	MaxLength$	 100��5�   �   ��6   �� *Nom+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��6�   �� 5�      $��1��6   $��0��6)   $��	_Prénom:��69    6L   �� $ 	txtPrenom���� 5�      -   5        $ 	txtPrenom��5�   ;   $��1��6   $��1��6+   $	 100��5�   �   ��6   �� *Prenom+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5�      $��2��6   $��0��6)   $��	_Adresse:��69   $	��Top=�6w   $��0,3,5,5q�6�    6M   �� $ 
txtAdresse��6�   �� 5�      -   5        $ 
txtAdresse��5�   ;   $��2��6   $��1��6+    �� AcceptsReturn$
 True��6O    �� TextWrapping$
 Wrap=�6d   $��60��6x   " �� VerticalScrollBarVisibility$
 Auto=�6�   $	 300��5�   �   ��6   �� *	Adresse+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5�      $��3��6   $��0��6)   $��_Email:��69    6K   �� $ txtEmail���� 5�      -   5        $ txtEmail��5�   ;   $��3��6   $��1��6+   $	 150��5�   �   ��6   �� *Email+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5�      $��4��6   $��0��6)   $��_Téléphone:��69    6O   �� $ txtTelephone���� 5�      -   5        $ txtTelephone��5�   ;   $��4��6   $��1��6+   $ 20��5�   �    � ToolTip$( "Numéro de téléphone (optionnel)��5�      ��5�      �� *	Telephone+$ TwoWay=�$ PropertyChanged=�$
 True��5�      �� 5�      $��5��6   $��0��6)   $��_Statut:��69    6L   �� $ 	cmbStatut���� 5�      -   5         ��Name$ 	cmbStatut��5�   <   $��5��6   $��1��6,   ��5�      �� *StatutsPossibles+ �� SelectedItem 5�      �� *Statut+$ TwoWay=�$
 True���� 5�      $��6��6   $��0��6)   $��Date d'inscription:��69   $��Center=�6W   $��0,5,5,5q�6r   �� 5�      $��6��6    $��1��6-   $��Center=�6=   $��0,5,0,5q�6X   $��Italic$�5�       
�������$) #Cette date ne peut être modifiée.��6E   ��5�       �� *DateInscriptionDisplay+ �� StringFormat$ {0:dd/MM/yyyy}��5�   E   5�      5�      5�      �� 5�   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�5�      �� $��_Sauvegarder��6    �� 	IsDefault$
 True��6R   $	��100��6c   $��0,0,10,0q�6r   ��6,   �� *SaveMemberCommand+{�6�   �� *	IsNotBusy+�� 5�      $��A_nnuler��6    �� IsCancel$
 True��6N   $	��100��6^   ��6(   �� *CancelEditCommand+{�6m   �� *	IsNotBusy+5�      � 5�   
   $��0��6   $��3��6   ���	#60FFFFFF K� ZIndex$ 10��6D   y�5�      �� *IsBusy+    BooleanToVisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.+�5�      �� 
���OO/� �� CornerRadius$ 5��60     �� Padding$  20q�6A   ! J� Opacity$	! 0.8Q�6N   $��Center=�5�      $��Center=�62   ._�5�      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5�      5�      5�      5�      