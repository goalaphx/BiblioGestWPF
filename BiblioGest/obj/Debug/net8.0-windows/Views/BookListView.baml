   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      #    BiblioGest.Views.BookListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5         RightAlignedCellStyle �� 
TargetType*  $System.Windows.Controls.DataGridCell �� Property �� Value��%  &	        '�� p     �� 5   
   "  6.   .�5      ��  � ��$ Right=�64   �� 5       � ��$	 5,2q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$
 10,5q�6(   �� 5       � ��$ 3q�6'   �� 5       � ��$ 80��6)   5      5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $��* �6   �� 5#      $
��Auto �6   .+�5'   
   �� $��0��6   ���	#FFE6FFE6 �� Padding$ 8q�65   $��0,0,0,5q�6A    �� CornerRadius$ 3��6R   
���q�<�$��1q�6�   ._�5(      �  � Orientation$ 
Horizontal=�6   .+�5)      �� $��_Ajouter��6    � ToolTip$ Ajouter un nouveau livre��6O   ��6,   �� *AddBookCommand+{�6r   �� *	IsNotBusy+�� 5*      $��	_Modifier��6   $% Modifier le livre sélectionné��6Q   ��6-   �� *EditBookCommand+{�6y   �� *	IsNotBusy+�� 5+      $��
_Supprimer��6   $&  Supprimer le livre sélectionné��6T   ��6.   �� *DeleteBookCommand+{�6}   �� *	IsNotBusy+�� 5,      $��Ac_tualiser��6   $��clear_search��6T   $��15,0,0,0q�6t   $. (Recharger la liste (efface la recherche)��6�   ��6/   �� *LoadBooksCommand+{�6�   �� *	IsNotBusy+5-      5.      � 51   
   $��1��6   $��0,5,0,8q�6   	 � ColumnDefinitions	 52      �� 53      $
��Auto �6#   �� 54      $��* �6#   �� 55      $
��Auto �6#   .+�57      �� $��_Rechercher:��6   $��0��6+   $��Center=�6;   $��0,0,5,0q�6V   
 �� Target
 6g   ��  �� ElementName$ txtSearchBooks���� 58      -   5         ��Name$ txtSearchBooks��58   &   $��1��6   $A ;Rechercher par Titre, Auteur, ISBN, Éditeur, ou Catégorie��5:      $��Center=�5;      $��3q�61   $��28��6=   $	��200��5<      ��59      �� *
SearchText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5<      �� 5>      $��2��6   $��X��6%   $ Effacer la recherche��61   $��5,0,0,0q�6P   $��28��6a   $��28��6l   ��5?      �� *ClearSearchCommand+y�5@      �� *
SearchText+ ) %StringToVisibilityConverterIfNotEmpty �� 	Converter# [  �� FallbackValue$ 	Collapsed��{�5A      �� *	IsNotBusy+5B      &   System.Windows.Controls.DataGrid  5E   
   $��2��6      AutoGenerateColumns$ False��5H         
IsReadOnly$
 True��5I         CanUserAddRows$ False��5J         SelectionMode$ Single=�5K      "   VerticalScrollBarVisibility$
 Auto=�5L      $   HorizontalScrollBarVisibility$
 Auto=�5M      
�������$��1q�5N   +      RowBackground
 �����   AlternatingRowBackground �	#FFF5FFF5��5F      �� *Livres+ �� SelectedItem 5G      �� *SelectedLivre+$ TwoWay=�   Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$
 ISBN��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6L      MinWidth$	 120Q�6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *ISBN+6Y     5R      $ Titre��6%   $
 2.5* 6N    64   �� *Titre+6N     5S      $ Auteur��6%   $
 1.5* 6P    65   �� *Auteur+6P     5T      $ 
Catégorie��6%   $ * 6Z   $	 100Q�6d    68   �� *Categorie.Nom+6d     5U      $ Dispo.��6%   $
 Auto 6f   $ 60Q�6s      	CellStyle# [  6�    65   �� *NombreExemplairesDisponibles+6�     5V      $ Total��6%   $
 Auto 6_   $ 60Q�6l   # [  6z    64   �� *NombreExemplairesTotal+6z     5W      $ Année��6%   $
 Auto 6Y   $ 60Q�6f   # [  6t    64   �� *AnneePublication+6t     5X      $ Éditeur��6%   $ * 6R   $	 100Q�6\    66   �� *	Editeur+6\   5Z      �� 5]   
   $��3��6   $��0,5,0,0q�6!   .@�5^      �� .��5_      �� ��6   �� *Livres.Count+  �� StringFormat$   {0} livre(s) trouvé(s)��5`      �� 5a      $��Right=�6   .��5b      � $	��100��6   $��15��6*   ! � IsIndeterminate$
! True��66   y�5c      �� *IsBusy+   BooleanToVisibilityConverter# [ $ 	Collapsed��5d      5e      � 5h   
   $��0��6   $��4��6   ���	#60FFFFFF" K� ZIndex$" 10��6D   y�5i      �� *IsBusy+# [ $ 	Collapsed��.+�5j      �� 
���OO/�$ 5��60   $ 20q�6A   # J� Opacity$	# 0.8Q�6N   $��Center=�5k      $��Center=�62   ._�5l      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5m      5n      5o      5p      