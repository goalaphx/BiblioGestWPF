   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5      #    BiblioGest.Views.LoanListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��$
 10,5q�6(   �� 5       � ��$ 3q�6'   �� 5       � ��$ 80��6)   5      5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5    
   �� $��0��6   ���	#FFFFF0E1 �� Padding$ 8q�65   $��0,0,0,5q�6A    �� CornerRadius$ 3��6R   
��� E��$��1q�6{   ._�5!      �  � Orientation$ 
Horizontal=�6   .+�5"      �� $��Nouvel Emprunt��6    � ToolTip$# Enregistrer un nouvel emprunt��6U   ��62   �� *NewLoanCommand+{�6}   �� *	IsNotBusy+�� 5#      $��Retourner Livre��6   $5 /Marquer l'emprunt sélectionné comme retourné��6Y   ��63   �� *ReturnBookCommand+{�6�   �� *	IsNotBusy+�� 5$      $��Rafraîchir��6   $��clear_search��6S   $��15,0,0,0q�6s   $. (Recharger la liste (efface la recherche)��6�   ��6.   �� *LoadLoansCommand+{�6�   �� *	IsNotBusy+5%      5&      � 5)   
   $��1��6   $��0,5,0,8q�6   	 � ColumnDefinitions	 5*      �� 5+      $
��Auto �6#   �� 5,      $��* �6#   �� 5-      $
��Auto �6#   .+�5/      �� $��Rechercher Emprunt:��6   $��0��62   $��Center=�6B   $��0,0,5,0q�6]   
 �� Target
 6n   ��  �� ElementName$ txtSearchLoans���� 50      -   5         ��Name$ txtSearchLoans��50   &   $��1��6   $E ?Rechercher par Titre de Livre, Nom/Prénom d'Adhérent, ou ISBN��52      $��Center=�53      $��3q�61   $��28��6=   $	��200��54      ��51      �� *
SearchText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�54      �� 55      $��2��6   $��X��6%   $ Effacer la recherche��61   $��5,0,0,0q�6P   $��28��6a   $��28��6l   ��56      �� *ClearSearchCommand+y�57      �� *
SearchText+ )  %StringToVisibilityConverterIfNotEmpty �� 	Converter# [   �� FallbackValue$ 	Collapsed��{�58      �� *	IsNotBusy+59      &   System.Windows.Controls.DataGrid  5<   
   $��2��6      AutoGenerateColumns$ False��5?         
IsReadOnly$
 True��6/      CanUserAddRows$ False��6A      CanUserDeleteRows$ False��6X      SelectionMode$ Single=�5@         HeadersVisibility$ Column=�6*      GridLinesVisibility$ 
Horizontal=�6E      RowBackground
 �����   AlternatingRowBackground �	#FFFFF8F0
�������$��1q�5B   +   ��5=      �� *Loans+ �� SelectedItem 5>      �� *SelectedLoan+$ TwoWay=�   Columns 5C      0  *System.Windows.Controls.DataGridTextColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ 
ID Emprunt��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P      MinWidth$ 80Q�6]      SortMemberPath$ Id��6k   1  +System.Windows.Controls.DataGridBoundColumn    Binding  69   �� *Id+6k     5E      $ Livre (Titre)��6%   $
 2.2* 6\   $ Livre.Titre��6i     6<   �� *Livre.Titre+6i     5F      $ 	Adhérent��6%   $
 1.5* 6_   $ Adherent.NomComplet��6l     67   �� *Adherent.NomComplet+6l     5G      $ Date Emprunt��6%   $
 Auto 6�   $	 130Q�6�   $ DateEmprunt��6�     6;   �� *DateEmprunt+! �� StringFormat$! {0:dd/MM/yyyy HH:mm}��6�     5H      $ Retour Prévu��6%   $
 Auto 6�   $	 100Q�6�   $ DateRetourPrevue��6�     6;   �� *DateRetourPrevue+$! {0:dd/MM/yyyy}��6�     5I      $ Retour Effectif��6%   $
 Auto 6�   $	 130Q�6�   $ DateRetourEffective��6�     6>   �� *DateRetourEffective+$! {0:dd/MM/yyyy HH:mm}��" �� TargetNullValue$" En cours��6�   4  .System.Windows.Controls.DataGridCheckBoxColumn  5J      $ 
En Retard?��6)   #   
IsReadOnly$
# True��6j   $
 Auto 6|   $ 80Q�6�     6=   �� *EstEnRetard+$ OneWay=�6�   5L      �� 5O   
   $��3��6   $��0,5,0,0q�6!   .@�5P      �� .��5Q      �� ��6   �� *Loans.Count+$!  {0} emprunt(s) listé(s)��5R      �� 5S      $��Right=�6   .��5T      � $	��100��6   $��15��6*   $ � IsIndeterminate$
$ True��66   y�5U      �� *IsBusy+   BooleanToVisibilityConverter# [ $ 	Collapsed��5V      5W      � 5Y   
   $��0��6   $��4��6   ���	#60FFFFFF% K� ZIndex$% 10��6D   y�5Z      �� *IsBusy+# [ $ 	Collapsed��.+�5[      �� 
���OO/�$ 5��60   $ 20q�6A   & J� Opacity$	& 0.8Q�6N   $��Center=�5\      $��Center=�62   ._�5]      �� $��Opération en cours...��6   $��14(�69   $��SemiBold"�6G   
�������6]   5^      5_      5`      5a      