   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      '    !BiblioGest.Views.CategoryListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��$
 10,5q�6(   �� 5       � ��$ 3q�6'   �� 5       � ��$ 80��6)   5      5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5!   
   �� $��0��6   ���	#FFE0E0FF �� Padding$ 8q�65   $��0,0,0,5q�6A    �� CornerRadius$ 3��6R   
����Zj�$��1q�6{   ._�5"      �  � Orientation$ 
Horizontal=�6   .+�5#      �� $��_Ajouter��6    � ToolTip$% Ajouter une nouvelle catégorie��6S   ��6,   �� *AddCategoryCommand+{�6|   �� *	IsNotBusy+�� 5$      $��	_Modifier��6   $+ %Modifier la catégorie sélectionnée��6U   ��6-   �� *EditCategoryCommand+{�6�   �� *	IsNotBusy+�� 5%      $��
_Supprimer��6   $, &Supprimer la catégorie sélectionnée��6X   ��6.   �� *DeleteCategoryCommand+{�6�   �� *	IsNotBusy+�� 5&      $��_Rafraîchir��6   $��clear_search��6Y   $��15,0,0,0q�6y   $. (Recharger la liste (efface la recherche)��6�   ��6/   �� *LoadCategoriesCommand+{�6�   �� *	IsNotBusy+5'      5(      � 5+   
   $��1��6   $��0,5,0,8q�6   	 � ColumnDefinitions	 5,      �� 5-      $
��Auto �6#   �� 5.      $��* �6#   �� 5/      $
��Auto �6#   .+�51      �� $��Rechercher Catégorie:��6   $��0��64   $��Center=�6D   $��0,0,5,0q�6_   
 �� Target
 6p   ��  �� ElementName$ txtSearchCategories���� 52      -   5         ��Name$ txtSearchCategories��52   &   $��1��6   $' !Rechercher par Nom ou Description��54      $��Center=�55      $��3q�61   $��28��6=   $	��200��56      ��53      �� *
SearchText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�56      �� 57      $��2��6   $��X��6%   $ Effacer la recherche��61   $��5,0,0,0q�6P   $��28��6a   $��28��6l   ��58      �� *ClearSearchCommand+y�59      �� *
SearchText+ )  %StringToVisibilityConverterIfNotEmpty �� 	Converter# [   �� FallbackValue$ 	Collapsed��{�5:      �� *	IsNotBusy+5;      &   System.Windows.Controls.DataGrid  5>   
   $��2��6      AutoGenerateColumns$ False��5A         
IsReadOnly$
 True��6/      CanUserAddRows$ False��6A      CanUserDeleteRows$ False��6X      SelectionMode$ Single=�5B         HeadersVisibility$ Column=�6*      GridLinesVisibility$ 
Horizontal=�6E      RowBackground
 �����   AlternatingRowBackground �	#FFF0F0FF
�������$��1q�5D   +   ��5?      �� *
Categories+ �� SelectedItem 5@      �� *SelectedCategorie+$ TwoWay=�   Columns 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H      MinWidth$ 50Q�6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6U     5G      $ Nom Catégorie��6%   $ 2* 6T    6<   �� *Nom+6T     5H      $ Description��6%   $ 3* 6Z    6:   �� *Description+6Z   5J      �� 5M   
   $��3��6   $��0,5,0,0q�6!   .@�5N      �� .��5O      �� ��6   �� *Categories.Count+  �� StringFormat$$   {0} catégorie(s) trouvée(s)��5P      �� 5Q      $��Right=�6   .��5R      � $	��100��6   $��15��6*   ! � IsIndeterminate$
! True��66   y�5S      �� *IsBusy+   BooleanToVisibilityConverter# [ $ 	Collapsed��5T      5U      � 5W   
   $��0��6   $��4��6   ���	#60FFFFFF" K� ZIndex$" 10��6D   y�5X      �� *IsBusy+# [ $ 	Collapsed��.+�5Y      �� 
���OO/�$ 5��60   $ 20q�6A   # J� Opacity$	# 0.8Q�6N   $��Center=�5Z      $��Center=�62   ._�5[      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5\      5]      5^      5_      