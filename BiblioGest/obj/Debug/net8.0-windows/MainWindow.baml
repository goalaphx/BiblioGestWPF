   M S B A M L   `   `   `  ����E  ABiblioGest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BiblioGest
BiblioGest  5   	   3clr-namespace:BiblioGest.ViewsBiblioGest.Views  5   	   =#clr-namespace:BiblioGest.ViewModelsBiblioGest.ViewModels  5   	   =#clr-namespace:BiblioGest.ConvertersBiblioGest.Converters  5	   	       BiblioGest.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:BiblioGest     5   	   0viewsclr-namespace:BiblioGest.Views     5   	   :
viewmodels#clr-namespace:BiblioGest.ViewModels     5   	   :
converters#clr-namespace:BiblioGest.Converters     5	   	     � Title$+  %BiblioGest - Gestion de Bibliothèque��5   	   $	��720��66   $
��1100��6C    � WindowStartupLocation$ CenterScreen=�5   	   $	��500��6.   $	��800��6>   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   $	��200��6,   �� 5      $��* �6   .+�5   
   �� $��0��6   ���	#FF303030���	#FF444444$��0,0,1,0q�6P   ._�5      �� $��5q�6   .+�5      �� $��
BiblioGest��6   
�������$��24(�6F   $��SemiBold"�6T   $��15,20,15,15q�6j   $��Center=�6    �� TextAlignment$ Center=�6�   �� 5      ���	#FF555555$��
10,0,10,15q�63   �� 5!      $��Tableau de Bord��6   $
��10,5q�63   $
��12,8q�6A   
����������	#FF404040$��0q�6   $
��Left=�6�    � ToolTip$! Afficher le tableau de bord��6�   $
{�True��5"   H   ��6   �� *NavigateToDashboardCommand+6H   �� 5$      $��Gestion Livres��6   $
��10,5q�62   $
��12,8q�6@   
����������	#FF404040$��0q�6~   $
��Left=�6�   $$ Gérer la collection de livres��6�   ��5%      �� *NavigateToBooksCommand+�� 5'      $��Gestion Adhérents��6   $
��10,5q�65   $
��12,8q�6C   
����������	#FF404040$��0q�6�   $
��Left=�6�   $, &Gérer les membres de la bibliothèque��6�   ��5(      �� *NavigateToMembersCommand+�� 5+      $��Gestion Emprunts��6   $
��10,5q�64   $
��12,8q�6B   
����������	#FF404040$��0q�6�   $
��Left=�6�   $" Gérer les prêts et retours��6�   ��5,      �� *NavigateToLoansCommand+�� 5.      $��Gestion Catégories��6   $
��10,5q�66   $
��12,8q�6D   
����������	#FF404040$��0q�6�   $
��Left=�6�   $&  Gérer les catégories de livres��6�   ��5/      �� *NavigateToCategoriesCommand+55      56      �� 59   
   $��1��6    �� Padding$ 20q�6!   ���	#FFEFEFEF._�5=      �� $��Stretch=�5>      $��Stretch=�6;   ��5=      �� *CurrentViewModel+5>   ;   5?      � 5C   
   $��2��6   ���	#7F000000 K� ZIndex$	 100��6:   y�5D      �� *CurrentViewModel.IsBusy+    BooleanToVisibilityConverter �� 	Converter# [   �� FallbackValue$ 	Collapsed��.+�5E      �� ���	#CC000000	 �� CornerRadius$	 10��6,   $ 35q�6>   $��Center=�5F      $��Center=�62   $	��180��6M   ._�5G      �� 
 �� Orientation$
 Vertical=�6   .+�5J      �  � IsIndeterminate$
 True��6"   $��50��69   $��50��6D    % !MaterialDesignCircularProgressBar#���  6P   
�������$��Center=�6�   �� 5M      $��Chargement en cours...��6    $��Center=�6>   $��0,20,0,0q�6Y   
�������$��14(�6�   $��Center=�6�   5N      5O      5P      5R      5S      