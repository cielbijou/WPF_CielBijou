   M S B A M L   `   `   `  ����D  @CielBijou, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CielBijou.ViewCielBijou.View  5      9!clr-namespace:CielBijou.ViewModelCielBijou.ViewModel  5      $    CielBijou.View.CommentaireView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:CielBijou.View     5      8
viewmodels!clr-namespace:CielBijou.ViewModel     5      
�������  � 	Resources  5       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 10q�6(   �� 5       � ��$ Center=�64   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��253* �6   �� 5      $
��222* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Liste des Commentaires��6   $��0��63   $��0��6C   $��10,10,10,10q�6P   .��5       &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridCommentaires��5          AutoGenerateColumns .  $��5q�5$         
IsReadOnly . ��5!      �� *CommentairesList+	 �� SelectedItem	 5"      �� *SelectedCommentaire+
   Columns
 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Nom Client��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *	ClientNom+6[     5'      $ Prénom Client��6)   $	 100 6a    6@   �� *ClientPrenom+6a     5(      $ 
ID Produit��6)   $ 80 6[    6=   �� *	ProduitID+6[     5)      $ Produit��6)   $ 80 6Y    6:   �� *
ProduitNom+6Y     5*      $
 Note��6)   $ 60 6P    67   �� *Note+6P     5+      $
 date��6)   $	 100 6P    67   �� *date+6P   5-      5.      �� 51   
   $��Détails du Commentaire��6   $��1��63   $��0��6C   $��10,10,10,10q�6P   .��52      �� .+�53      ��  �� Orientation$ 
Horizontal=�6   $��5q�66   .+�54      �� $��ID Commentaire��6   $��Center=�65   $	��120��6P   �� 55      -   5         ��Name$ TextBoxNomIdCommentaire��56      $��5q�57      $��60��6)   ��55      �� *&$SelectedCommentaire.leCommentaire.id+57   )   58      �� 5:      $ 
Horizontal=�6   $��5q�66   .+�5;      �� $��
Nom Client��6   $��Center=�61   $	��120��6L   �� 5<      -   5        $ TextBoxNomClient��5=      $	��200��68   $��5q�6D   ��5<      �� *.,SelectedCommentaire.leCommentaire.client.nom+5=   D   5>      �� 5@      $ 
Horizontal=�6   $��5q�66   .+�5A      �� $��Prénom Client��6   $��Center=�64   $	��120��6O   �� 5B      -   5        $ TextBoxPrenomClient��5C      $	��200��6;   $��5q�6G   ��5B      �� *1/SelectedCommentaire.leCommentaire.client.prenom+5C   G   5D      �� 5F      $ 
Horizontal=�6   $��5q�66   .+�5G      �� $��Produit��6   $��Center=�6.   $	��120��6I   �� 5H      -   5        $ TextBoxNomProduit��5I      $	��200��69   $��5q�6E   ��5H      �� *42SelectedCommentaire.leCommentaire.produit.nom_prod+5I   E   5J      �� 5L      $ 
Horizontal=�6   $��5q�66   .+�5M      �� $��Catégorie du Produit��6   $��Center=�6;   $	��120��6V   �� 5N      -   5        $ TextBoxlaCategorieProduit��5O      $	��200��6A   $��5q�6M   ��5N      �� *A?SelectedCommentaire.leCommentaire.produit.categorie.libelle_cat+5O   M   5P      �� 5R      $ 
Horizontal=�6   $��5q�66   .+�5S      �� $��Commentaire��6   $	��top=�62   $	��120��6J   �� 5T      {�.  $	��270��5U   0   $��80��6<   " �� VerticalScrollBarVisibility$
 Auto=�6H    �� AcceptsReturn . $��5q�6�   ��5T      �� *75SelectedCommentaire.leCommentaire.contenu_commentaire+5U   �   5V      �� 5X      $ 
Horizontal=�6   $��5q�66   .+�5Y      �� $
��Date��6   $��Center=�6+   $	��120��6F   �� 5Z      {�.  $	��200��5[   0   $��5q�6<   ��5Z      �� *42SelectedCommentaire.leCommentaire.date_commentaire+ �� StringFormat$ {0:dd/MM/yyyy}��5[   <   5\      �� 5^      $ 
Horizontal=�6   $��5q�66   .+�5_      �� $
��Note��6   $��Center=�6+   $	��120��6F   �� 5`      {�.  $	��200��6t   $��5q�6�   ��6   �� *42SelectedCommentaire.leCommentaire.note_commentaire+6�   5a      �� 5c      $ 
Horizontal=�6   $��5q�66   .+�5d      �� $��Statut��6   $��Center=�6-   $	��120��6H   �� 5e      $	��200��5g      $��5q�6+   	 5e      �� *64SelectedCommentaire.leCommentaire.status_commentaire+��5f      �� *StatutOptions+5g   +   5h      �� 5k      $ 
Horizontal=�6   $��Center=�66   $��5q�6S   .+�5l      �� $��	Supprimer��6   $��5q�6V   ��61   �� *SupprimerCommand+6V   5m      5n      5o      5p      5q      