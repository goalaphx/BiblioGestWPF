   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5      5clr-namespace:BiblioGest.ModelsBiblioGest.Models  5      =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5      =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5	      $    BiblioGest.Views.DashboardView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:BiblioGest.Views     5      2modelsclr-namespace:BiblioGest.Models     5      :
viewmodels#clr-namespace:BiblioGest.ViewModels     5      :
converters#clr-namespace:BiblioGest.Converters     5	      ���	#FFF0F2F5  � 	Resources  5       �� 
TargetType   StatCardStyle �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   Color   Opacity   
BlurRadius  StatValueStyle  StatLabelStyle  SectionHeaderStyle��%[  &	        &	 �    &	 �    &	 h    �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � �� �	#FFDDE2E66,   �� 5       � ��$ 1q�60   �� 5       �@��CornerRadius$ 8��6-   �� 5       �@��Padding$ 20q�6(   �� 5       � ��$ 8q�6'   �� 5       � ��$	 180��6)   �� 5       � ��$	 110��6*   �� 5       �@J�Effect 5        5      $ 2Q�6'   $ 	#FFAAAAAA��67   $	 0.2Q�6I   $ 6Q�6W   5      5       �� 5"   
   " ��6   .�5#      ��  � ��$ 28(�6)   �� 5$       � ��$ SemiBold"�6+   �� 5%       � ��$
 Left=�64   �� 5&       � ��$ 0,0,0,5q�6'   �� 5'       � �� �	#FF303F9F6+   5(      �� 5*   
   " ��6   .�5+      ��  � ��$ 13(�6)   �� 5,       � ��$
 Left=�64   �� 5-       � �� �	#FF546E7A6+   5.      �� 50   
   " ��6   .�51      ��  � ��$ 18(�6)   �� 52       � ��$ SemiBold"�6+   �� 53       � ��$ 10,20,10,10q�6'   �� 54       � �� �	#FF4242426+   55      56      .��58      �� $
��Auto=�6   $��Disabled=�66   .��59   
   � $��15q�6    � RowDefinitions 5:      �� 5;      $
��Auto �6    �� 5<      $
��Auto �6    �� 5=      $
��Auto �6    �� 5>      $
��Auto �6    �� 5?      $
��Auto �6    �� 5@      $
��Auto �6    �� 5A      $��* �6    .+�5D      �� $��0��6   $��22(�6E   $��Normal"�6S   $
��Left=�5E      $��	10,5,0,20q�63   ���	#FF37474F��5D   %   �� *WelcomeMessage+5E   F   � 5G      $��1��6   	 � Orientation$	 
Horizontal=�6%   $��Stretch=�6>   $��0,0,0,10q�6\   .+�5H      �� #��[  6   ._�5I      �� .+�5J      �� #��[ 6A   ��6$   �� *TotalLivres+6A   �� 5K      $��Titres de Livres��6$   #��[ 6<   5L      5M      �� 5N      #��[  6   ._�5O      �� .+�5P      �� #��[ 6G   ��6$   �� *LivresDisponibles+6G   �� 5Q      $��Exemplaires Disponibles��6$   #��[ 6C   5R      5S      �� 5T      #��[  6   ._�5U      �� .+�5V      �� #��[ 6J   ��6$   �� *TotalAdherentsActifs+6J   �� 5W      $��Adhérents Actifs��6$   #��[ 6<   5X      5Y      �� 5Z      #��[  6   ._�5[      �� .+�5\      �� #��[ 6E   ��6$   �� *EmpruntsEnCours+6E   �� 5]      $��Emprunts en Cours��6$   #��[ 6=   5^      5_      �� 5`      #��[  6   ._�5a      �� .+�5b      �� #��[ 5c   $   ��5b   $   �� *EmpruntsEnRetard+��5d   $   �� *EmpruntsEnRetard+  MyZeroToBrushConverter
 �� 	Converter#
 [ �� 5f      $��Emprunts en Retard��6$   #��[ 6>   5g      5h      5i      �� 5k      $��2��6   $��Livres par Catégorie��6%   #��[ 6A   �� 5l      $��3��6   #��[  6"    �� Padding$ 15q�6I   $��
10,0,10,10q�6V   $��0��6j   ._�5m      �� ��6   �� *CategoryBookCounts+��5n      �� 5o       �� DataType6   0BiblioGest.ViewModels.CategoryBookCountViewModel"  6'   .R�5p      � $	��0,3q�6#    � ColumnDefinitions 5q   "   �� 5r   &   $
��Auto �67   $	��150��6D   �� 5s   &   $��* �67   �� 5t   &   $
��Auto �67   $��50��6D   .+�5v   "   �� $��0��6,   $��Center=�6Z   $��CharacterEllipsis=�6�   ��6<   �� *CategoryName+ � ToolTip 6u   �� *CategoryName+6�   � 5w   "   $��1��6.    � Maximum$	 100Q�6[   $��18��6i   $
��10,0q�6u   $��Center=�6�   
������� � Value 6>   �� *
Percentage+��6�   �� *
BarColor+�� 5x   "   $��2��6,   $��Center=�6W   $��Right=�6r   $��5,0,0,0q�6�   ��6<   �� *	BookCount+6�   5y      5z      5|      5}      �� 5      $��4��6   $��Emprunts Récents��6%   #��[ 6=   �� 5�      $��5��6   #��[  6"   $ 15q�6I   $��
10,0,10,10q�6V   $��0��6j   ._�5�      �� ��6   �� *RecentLoans+��5�      �� 5�         BiblioGest.Models.Emprunt"  6'   .R�5�      ��  �� Orientation$ 
Horizontal=�6)   $	��0,2q�6B   .+�5�   "   �� $��SemiBold"�6I   $	��250��6_   $��CharacterEllipsis=�6n   ��6,   �� *Livre.Titre+ 6�   �� *Livre.Titre+�� 5�   "   $�� par ��6,   $	��5,0q�69   �� 5�   "   $��Italic$�6Q   $	��150��6d   $��CharacterEllipsis=�6s   ��6,   �� *Adherent.NomComplet+ 6�   �� *Adherent.NomComplet+�� 5�   "   $
�� le ��6,   $	��5,0q�68   �� 5�   "   ��6,   �� *DateEmprunt+ �� StringFormat$ dd/MM/yy HH:mm��5�      5�      ��5�      �� 5�      " ��6     �� Triggers 5�      �� 5�   "    �� Binding 6.   �� *RecentLoans.Count+ �� Value 06T   .a�5�   &   ��  � �� 5�   *   �� 5�   .   .R�5�   2   �� $(��"Aucun emprunt récent à afficher.��6<   
�������$��Center=�6v   $��Center=�6�   5�   /   5�   '   5�   #   5�      5�      5�      � 5�      $��0��6   $��7��6    ���	#30FFFFFF K� ZIndex$	 100��6H   $��Stretch=�6[   $��Stretch=�6w   y�5�      �� *IsBusy+   BooleanToVisibilityConverter#
 [  �� FallbackValue$ 	Collapsed��.+�5�      �� 
���iii� �� CornerRadius$ 5��6.   $ 25q�6?    J� Opacity$	 0.9Q�6L   $��Center=�5�      $��Center=�66   ._�5�      �� $ 
Horizontal=�6!   .+�5�      �  � IsIndeterminate$
 True��6&    % !MaterialDesignCircularProgressBar#���  6=   $��30��6y   $��30��6�   
�������6�   �� 5�      $ ��Chargement des données...��6$   $��15,0,0,0q�6E   $��Center=�6W   
�������$��14(�6�   5�      5�      5�      5�      5�      5�      