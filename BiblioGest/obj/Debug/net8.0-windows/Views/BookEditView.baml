   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      #    BiblioGest.Views.BookEditView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value ��Name �� ElementName �� Path �� Triggers P� Property	 P� Value��%�  '��       '�� �    '��     �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,5q�6'   �� 5       � ��$ 2q�6(   �� 5       �@=�ErrorTemplate 5      �� 5      .R�5      �� .+�5      �� $ controlPlaceholder��68   �� 5      
���  ��$��10(�6�   $��0,1,0,0q�6�   ��6+   �� $ controlPlaceholder��$( "AdornedElement.(Validation.Errors)���5   "   �� 5   &   .R�5   *   �� ��64   �� *ErrorContent+5   '   5!      5"      5#      5%       5&      P� 5'       �@=�HasError$
	 True��69   . �5(      ��  � ��
 �  ��64   �� 5)       � ��$ 1q�68   5-      5/      �� 50   
   " ��6   .�51      ��  � ��$ 0,0,0,5q�6(   �� 52       � ��$ 2q�6)   �� 53       �@=�ErrorTemplate 54      �� 55      .R�56      �� .+�57      �� $ controlPlaceholder��68   �� 58      
���  ��$��10(�6�   $��0,1,0,0q�6�   ��6+   �� $ controlPlaceholder��$( "AdornedElement.(Validation.Errors)���59   "   �� 5:   &   .R�5;   *   �� ��64   �� *ErrorContent+5<   '   5>      5?      5@      5B       5C      P� 5D       �@=�HasError$
	 True��69   . �5E      ��  � ��
 �  ��64   �� 5F       � ��$ 1q�68   5J      5L      �� 5M   
   " ��6   .�5N      ��  � ��$ Center=�62   �� 5O       � ��$ 0,0,10,5q�6'   5P      5Q      .��5S      � $��20q�6   
 � RowDefinitions
 5T   
   �� 5U      $
��Auto �6   �� 5V      $��* �6   �� 5W      $
��Auto �6   .+�5Z   
   �� $��0��6   $��20(�6>   $��SemiBold"�6L   $��0,0,0,15q�6b   $��Center=�6t   ��6!   �� *WindowTitle+6t   �� 5\   
   $��1��6   $
��Auto=�6$   $��0,0,5,0q�6G   .��5]      �  � ColumnDefinitions 5^      �� 5_      $
��Auto �6'   $	��120��64   �� 5`      $��* �6'   
 5b      �� 5c      $
��Auto �6$   �� 5d      $
��Auto �6$   �� 5e      $
��Auto �6$   �� 5f      $
��Auto �6$   �� 5g      $
��Auto �6$   �� 5h      $
��Auto �6$   �� 5i      $
��Auto �6$   �� 5j      $
��Auto �6$   .+�5m      �� $��0��6   $��0��6%   $��_ISBN:��65    �� Target 6F   �� $ txtIsbn���� 5n      -   5         ��Name$ txtIsbn��5n   7   $��0��6   $��1��6'    �� 	MaxLength$ 20��5o   �   ��6   �� *Isbn+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��6�   �� 5q      $��1��6   $��0��6%   $��_Titre:��65    6G   �� $ txtTitre���� 5r      -   5        $ txtTitre��5r   7   $��1��6   $��1��6'   $	 250��5s   �   ��6   �� *Titre+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5u      $��2��6   $��0��6%   $��_Auteur:��65    6H   �� $ 	txtAuteur���� 5v      -   5        $ 	txtAuteur��5v   7   $��2��6   $��1��6'   $	 200��5w   �   ��6   �� *Auteur+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5y      $��3��6   $��0��6%   $��
É_diteur:��65    6I   �� $ 
txtEditeur���� 5z      -   5        $ 
txtEditeur��5z   7   $��3��6   $��1��6'   $	 150��5{   �   ��6   �� *	Editeur+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5}      $��4��6   $��0��6%   $��_Catégorie:��65    6K   �� $ cmbCategorie���� 5~      -   5         ��Name$ cmbCategorie��5~   8   $��4��6   $��1��6(    �� DisplayMemberPath$	 Nom��5�      ��5      �� *
Categories+ �� SelectedItem 5�      �� *SelectedCategorie+$ TwoWay=�$
 True��5�      �� 5�      $��5��6   $��0��6%   $��_Année Publ.:��65    6M   �� $ txtAnnee���� 5�      -   5        $ txtAnnee��5�   7   $��5��6   $��1��6'   $ 4��5�   �   ��6   �� *AnneePublication+$ TwoWay=�$ PropertyChanged=�$
 True��6�   �� 5�      $��6��6   $��0��6%   $��Nbre E_x. Total:��65    6P   �� $ txtTotal���� 5�      -   5        $ txtTotal��5�   7   $��6��6   $��1��6'   ��5�      �� *NombreExemplairesTotal+$ TwoWay=�$ PropertyChanged=�$
 True���� 5�      $��7��6   $��0��6%   $��Nbre Ex. _Dispo:��65    6P   �� $ txtDispo���� 5�      -   5        $ txtDispo��5�   7   $��7��6   $��1��6'   ��5�      �� *NombreExemplairesDisponibles+$ TwoWay=�$ PropertyChanged=�$
 True��5�      5�      �� 5�   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�5�      �� $��Enregistrer��6    �� 	IsDefault$
 True��6o   $	��100��6�   $��0,0,10,0q�6�   $
��10,5q�6�   ��6+   �� *SaveBookCommand+{�6O   �� *	IsNotBusy+6�   �� 5�      $��Annuler��6    �� IsCancel$
 True��6M   $	��100��6]   $
��10,5q�6l   ��6'   �� *CancelEditCommand+{�6{   �� *	IsNotBusy+5�      5�      5�      