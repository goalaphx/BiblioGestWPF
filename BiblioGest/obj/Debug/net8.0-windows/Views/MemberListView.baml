   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      %    BiblioGest.Views.MemberListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
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
   �� $��0��6   ���	#FFD8FFD8 �� Padding$ 8q�65   $��0,0,0,5q�6A    �� CornerRadius$ 3��6R   
���"�"�$��1q�6}   ._�5"      �  � Orientation$ 
Horizontal=�6   .+�5#      �� $��_Ajouter��6    � ToolTip$" Inscrire un nouvel adhérent��6Q   ��6,   �� *AddMemberCommand+{�6w   �� *	IsNotBusy+�� 5$      $��	_Modifier��6   $( "Modifier l'adhérent sélectionné��6S   ��6-   �� *EditMemberCommand+{�6}   �� *	IsNotBusy+�� 5%      $��
_Supprimer��6   $) #Supprimer l'adhérent sélectionné��6V   ��6.   �� *DeleteMemberCommand+{�6�   �� *	IsNotBusy+�� 5&      $��_Rafraîchir��6   $��clear_search��6V   $��15,0,0,0q�6v   $. (Recharger la liste (efface la recherche)��6�   ��6/   �� *LoadMembersCommand+{�6�   �� *	IsNotBusy+5'      5(      � 5+   
   $��1��6   $��0,5,0,8q�6   	 � ColumnDefinitions	 5,      �� 5-      $
��Auto �6#   �� 5.      $��* �6#   �� 5/      $
��Auto �6#   .+�51      �� $��Rechercher Adhérent:��6   $��0��63   $��Center=�6C   $��0,0,5,0q�6^   
 �� Target
 6o   ��  �� ElementName$ txtSearchMembers���� 52      -   5         ��Name$ txtSearchMembers��52   &   $��1��6   $A ;Rechercher par Nom, Prénom, Email, Téléphone, ou Adresse��54      $��Center=�55      $��3q�61   $��28��6=   $	��200��56      ��53      �� *
SearchText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�56      �� 57      $��2��6   $��X��6%   $ Effacer la recherche��61   $��5,0,0,0q�6P   $��28��6a   $��28��6l   ��58      �� *ClearSearchCommand+y�59      �� *
SearchText+ )  %StringToVisibilityConverterIfNotEmpty �� 	Converter# [   �� FallbackValue$ 	Collapsed��{�5:      �� *	IsNotBusy+5;      &   System.Windows.Controls.DataGrid  5>   
   $��2��6      AutoGenerateColumns$ False��5A         
IsReadOnly$
 True��6/      CanUserAddRows$ False��6A      CanUserDeleteRows$ False��6X      SelectionMode$ Single=�5B         HeadersVisibility$ Column=�6*      GridLinesVisibility$ 
Horizontal=�6E      RowBackground
 �����   AlternatingRowBackground �	#FFF8FFF8
�������$��1q�5D   +   ��5?      �� *	Adherents+ �� SelectedItem 5@      �� *SelectedAdherent+$ TwoWay=�   Columns 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H      MinWidth$ 40Q�6U      SortMemberPath$ Id��6c   1  +System.Windows.Controls.DataGridBoundColumn    Binding  61   �� *Id+6c     5G      $	 Nom��6%   $
 1.2* 6J   $	 Nom��6W     62   �� *Nom+6W     5H      $ Prénom��6%   $
 1.2* 6P   $ Prenom��6]     65   �� *Prenom+6]     5I      $ Email��6%   $ 2* 6N   $ Email��6Y     64   �� *Email+6Y     5J      $ Téléphone��6%   $ * 6V   $	 100Q�6`   $ 	Telephone��6o     68   �� *	Telephone+6o     5K      $ 
Inscrit le��6%   $
 Auto 6~   $ 90Q�6�   $ DateInscription��6�     69   �� *DateInscription+! �� StringFormat$! {0:dd/MM/yyyy}��6�     5L      $ Statut��6%   $ * 6P   $ 70Q�6Z   $ Statut��6h     65   �� *Statut+6h   5N      �� 5Q   
   $��3��6   $��0,5,0,0q�6!   .@�5R      �� .��5S      �� ��6   �� *Adherents.Count+$"!  {0} adhérent(s) trouvé(s)��5T      �� 5U      $��Right=�6   .��5V      � $	��100��6   $��15��6*   " � IsIndeterminate$
" True��66   y�5W      �� *IsBusy+   BooleanToVisibilityConverter# [ $ 	Collapsed��5X      5Y      � 5\   
   $��0��6   $��4��6   ���	#60FFFFFF# K� ZIndex$# 10��6D   y�5]      �� *IsBusy+# [ $ 	Collapsed��.+�5^      �� 
���OO/�$ 5��60   $ 20q�6A   $ J� Opacity$	$ 0.8Q�6N   $��Center=�5_      $��Center=�62   ._�5`      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5a      5b      5c      5d      