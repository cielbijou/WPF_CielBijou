   M S B A M L   `   `   `  ����D  @CielBijou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CielBijou	CielBijou  5   	   /clr-namespace:CielBijou.ViewCielBijou.View  5   	       CielBijou.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CielBijou     5   	   .viewsclr-namespace:CielBijou.View     5   	     � Title$  
Ciel Bijou��5
   	   $	��750��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.    � AllowsTransparency . 
������ .��5      �� 
������� �� CornerRadius$ 30��6    ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#   .+�5      �  � RowDefinitions 5      �� 5      $	��124 �6$   �� 5      .+�5      �� $��
28,44,0,50q�6      	topButton#��[  6-   $��30��6P   .��5      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$
 Menu=�6,   $��20��68   $��20��6C   5      �� 5       $��1��6   ���#623ed0$ 	0 50 0 30��6;   ._�5!      ]� .+�5#      �� $��80��6!   $��80��6,    �� Padding$ 3q�68   
�������$	 100��6W   $	��Top=�6j   $
��Left=�6   $��
20 -30 0 0q�6�   ._�5$      A� ��5%   "   �� 5&   &   	 �� ImageSource$	 /Resources/Ciel.png��61   5(      5)      �� 5,      $��
Ciel Bijou��6$   
�������$��22(�6I   $��SemiBold"�6W   $	��Top=�6m   
 �� TextAlignment$
 Center=�6�   $��	5 25 0 10q�6�   �� 5-      $"��Gestion de la vente de bijou��6$   ���#d0bfff$��14(�6]   $��SemiBold"�6k   $	��Top=�6�   $
 Center=�6�   �� 5/      $��25q�6$   $	��Top=�60   $	��0.8��6E   ���#7e5fdc6R   �� 52      $	��Top=�6%   .+�53      �� -   5          DashBoardButton#��[ 53   %   .��54   "   ��  �� Orientation$ 
Horizontal=�6-   .+�55   &     $ HomeVariant=�6<     DashBoardButtonIcon#��[ 6O   �� 56   &   $��Accueil��60   57   #   58      �� 5:      -   5        #��[ 5:   %   .��5;   "   �� $ 
Horizontal=�6-   .+�5<   &     $ Necklace=�6<   #��[ 6L   �� 5=   &   $��
Catégorie��60   5>   #   5?      �� 5D      -   5        #��[ 5D   %   .��5E   "   �� $ 
Horizontal=�6-   .+�5F   &     $ AccountMultipleOutline=�6<   #��[ 6Z   �� 5G   &   $��Clients��60   5H   #   5I      �� 5L      -   5        #��[ 5L   %   .��5M   "   �� $ 
Horizontal=�6-   .+�5N   &     $ ClipboardTextOutline=�6<   #��[ 6X   �� 5O   &   $��Commande��60   5P   #   5Q      �� 5S      -   5        #��[ 5S   %   .��5T   "   �� $ 
Horizontal=�6-   .+�5U   &     $ ChatProcessing=�6<   #��[ 6R   �� 5V   &   $��Commentaire��60   5W   #   5X      �� 5\      -   5        #��[ 5\   %   .��5]   "   �� $ 
Horizontal=�6-   .+�5^   &     $ StoreOutline=�6<   #��[ 6P   �� 5_   &   $��Produits��60   5`   #   5a      �� 5c      -   5        #��[ 5c   %   .��5d   "   �� $ 
Horizontal=�6-   .+�5e   &     $ 
TagOutline=�6<   #��[ 6N   �� 5f   &   $��
Promotions��60   5g   #   5h      5k      �� 5m      -   5         ��Name$ BntDeconnection��5m   !   $y�Hidden=�6:   #��[ 6N   $��Bottom=�6w   $��Bottom=�6�   $
��15 0q�6�   .��5n      �� $ 
Horizontal=�6)   .+�5o   "     $ Logout=�68   #��[ 6F   �� 5p   "   $��Déconnexion��6,   5q      5r      5u      5v      5w      � 5z      $��1��6   $��30 20 20 20q�6#    5{      �� 5|      $
��auto �6$   �� 5}      $
��auto �6$   �� 5~      $
��auto �6$   �� 5      $
��auto �6$   �� 5�      $
��auto �6$   �� 5�      $��* �6$   �� 5�      $
��auto �6$   .+�5�      � $	��300��6   $
��Left=�6#   .+�5�      �� $
��20 0q�6    $��Center=�6.   ���#b0b9c6 K� ZIndex$ 1��6^   $��Chercher ici ...��6o   �� 5�      -	   5         ��Name$ 
textSearch��5�       
 Search#��[ 62     5�      $ Magnify=�6,   $��15��6;   $��15��6F   $��Center=�6R   $��0 0 15 0q�6m   ���#b0b9c6$��Right=�6�   5�      �� 5�      -
   5        #��[  5�      $��0 0 20 0q�6<   $��Right=�6N   .��5�        $ Close=�6,   5�      �� 5�      #��[  6   $��0 0 55 0q�6<   ���#dcdcdc$��Right=�6c   .��5�        $	 Cog=�6,   5�      �� 5�      #��[  6   $��0 0 85 0q�6<   $��Right=�6N   .��5�        $ 	BellAlert=�6,   5�      �� 5�      $��1��6   ���#dae2ea$��
0,42,0,579q�6<   $��5��6P   ]� 5�      -   5         ]�Name$ 	DockPanel��5�      $��	0,57,0,10q�6/   $��5��6B   .+�5�          CielBijou.View.AcceuilView  5�      5�      5�      5�      5�      