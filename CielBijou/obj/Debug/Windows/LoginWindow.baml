   M S B A M L   `   `   `  ����D  @CielBijou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PPE.Wpf.WindowsPPE.Wpf.Windows  5   	   !    PPE.Wpf.Windows.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:PPE.Wpf.Windows     5   	     � WindowStartupLocation$  CenterScreen=�5       � WindowStyle$
 None=�5	   	    � AllowsTransparency . 
������  � Title$ LoginWindow��5
   	   $	��620��6   $	��800��6*   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��500 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��1��5      $��/Resources/Close.png��6     K� ZIndex$ 1��6>      
ImageClose#��[  6g   �� 5   
   
�������$��1��6$    �� CornerRadius$ 0 0 0 0��64   $��Right=�6K   $	��500��6g   ._�5      �� $��Center=�6   .+�5      �� $��Connectez-vous��6     	titleText#��[ 62   ���#7b5cd66U   �� 5       �� Orientation$ 
Horizontal=�6   $
��0 20q�66   $��Center=�6D   .+�5      �� $��/Resources/Ciel.png��6   $	��100��69   $	��100��6E   5      �� 5      $��1q�6   ���#acb0af$
��70 7q�6C   $ 5��6Q   ._�5      � $	��7 9q�6    5      �� 5      $
��Auto �6/   �� 5       $��* �6/   .+�5#      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ EmailOutline=�60   �� 5$      -   5        	 ��Name$	 	textEmail��5$   $   $��Email��6W     textHint#��[ 6d   �� 5%      -   5        
 ��Name$
 txtEmail��5%   "     textBox#��[ 6W   5&      5'      �� 5*      $��1q�6   ���#acb0af$
��70 7q�6C   $ 5��6Q   ._�5+      � $	��7 9q�6    5,      �� 5-      $
��Auto �6/   �� 5.      $��* �6/   .+�51        $ LockOutline=�60   �� 52      -   5        $	 textPassword��52   $   $��Mot de passe��6]   #��[ 6q   G� 53      -   5         G�Name$ txtPassword��53   &   #��[ 6e   54      55      �� 57      -   5         ��Name$ BtnConnexion��57      $��	Connexion��6/     
mainButton#��[ 6C   6g   59      5:      5;      5<      