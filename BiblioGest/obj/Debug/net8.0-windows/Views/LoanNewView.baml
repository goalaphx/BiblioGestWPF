   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      "    BiblioGest.Views.LoanNewView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value ��Name �� ElementName �� Path �� Triggers P� Property	 P� Value(  "System.Windows.Controls.DatePicker��%e  '��       '�� v     '  �    �� 5   
   " ��6   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 0,0,10,5q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,10q�6'   �� 5       � ��$ 2q�6(   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      �� .+�5      �� $ controlPlaceholder��68   �� 5      
���  ��$��10(�6�   $��0,1,0,0q�6�   ��6+   �� $ controlPlaceholder��$( "AdornedElement.(Validation.Errors)���5   "   �� 5   &   .R�5   *   �� ��64   �� *ErrorContent+5    '   5"      5#      5$      5&       5'      P� 5(       �@=�HasError$
	 True��69   . �5)      ��  � ��
 �  ��64   �� 5*       � ��$ 1q�68   5+      5-      �� 5.   
   "  6   .�5/      ��  � ��$ 0,0,0,10q�6'   50      51      .��53      � $��20q�6   
 � RowDefinitions
 54   
   �� 55      $
��Auto �6   �� 56      $��* �6   �� 57      $
��Auto �6   .+�5:   
   �� $��0��6   $��20(�6>   $��SemiBold"�6L   $��0,0,0,15q�6b   $��Center=�6t   ��6!   �� *WindowTitle+6t   �� 5<   
   $��1��6   $
��Auto=�6$   .��5=      �  � ColumnDefinitions 5>      �� 5?      $
��Auto �6'   $	��120��64   �� 5@      $��* �6'   
 5B      �� 5C      $
��Auto �6$   �� 5D      $
��Auto �6$   �� 5E      $
��Auto �6$   �� 5F      $
��Auto �6$   .+�5I      �� $��0��6   $��0��6%   $��
Adhérent:��65    �� Target 6I   �� $ cmbAdherent���� 5J      -   5         ��Name$ cmbAdherent��5J   8   $��0��6   $��1��6(    �� DisplayMemberPath$ 
NomComplet��5M       � ToolTip$* $Sélectionner l'adhérent emprunteur��5N      ��5K      �� *	Adherents+ �� SelectedItem 5L      �� *SelectedAdherent+" �� ValidatesOnNotifyDataErrors$
 True��5N      �� 5P      $��1��6   $��0��6%   $��Livre:��65    6F   �� $ cmbLivre���� 5Q      -   5        $ cmbLivre��5Q   8   $��1��6   $��1��6(   $ Titre��5T      $U OSélectionner le livre à emprunter (seuls les livres disponibles sont listés)��5U      ��5R      �� *AvailableLivres+ 5S      �� *SelectedLivre+$
 True��5U      �� 5W      $��2��6   $��0��6%   $��Date d'emprunt:��65    6O   �� $ dpDateEmprunt��  5X      -   5          Name$ dpDateEmprunt��5X   :   $��2��6   $��1��6*   $- 'Date à laquelle le livre est emprunté��5Z         SelectedDate 5Y      �� *DateEmprunt+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5Z      �� 5\      $��3��6   $��0��6%   $��Retour Prévu le:��65    6P   �� $ dpDateRetourPrevue��  5]      -   5        $ dpDateRetourPrevue��5]   :   $��3��6   $��1��6*   $4 .Date à laquelle le livre doit être retourné��5_       5^      �� *DateRetourPrevue+$ TwoWay=�$ PropertyChanged=�5_      5a      5b      �� 5d   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�5e      �� $��Enregistrer Emprunt��6    �� 	IsDefault$
 True��6w   $	��130��6�   $��0,0,10,0q�6�   $
��10,5q�6�   ��63   �� *SaveLoanCommand+{�6W   �� *	IsNotBusy+6�   �� 5f      $��Annuler��6    �� IsCancel$
 True��6I   $	��100��6Y   $
��10,5q�6h   ��6'   �� *CancelCommand+{�6w   �� *	IsNotBusy+5g      � 5j   
   $��0��6   $��3��6   ���	#60FFFFFF K� ZIndex$ 10��6D   y�5k      �� *IsBusy+    BooleanToVisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.+�5l      �� 
���OO/� �� CornerRadius$ 5��60    �� Padding$ 20q�6A    J� Opacity$	 0.8Q�6N   $��Center=�5m      $��Center=�62   ._�5n      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5o      5p      5q      5r      