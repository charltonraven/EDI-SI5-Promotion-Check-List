   M S B A M L   `   `   `  ����W  SEDI SI5 Promotion Check List, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:EDI_SI5_Promotion_Check_ListEDI_SI5_Promotion_Check_List  5   	   4    .EDI_SI5_Promotion_Check_List.EmailPasswordForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:EDI_SI5_Promotion_Check_List     5   	   -   5          �Name$  EmailPasswordWindow��5   	    � Title$ Authenticate��5   	   $	��266��6   $	��409��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��173* �6   �� 5      $
��132* �6   �� 5      $	��61* �6   .+�5   
   �� -   5         ��Name$ lblEmail��5      $��Email:��6"   $
��Left=�63   $��	57,70,0,0q�6N   $	��Top=�6a   $��16(�6y   $��31��6�   $��51��6�   �� 5   
   -   5        $ lblPassword��5      $��	Password:��6%   $
��Left=�69   $��
28,129,0,0q�6T   $	��Top=�6h   $��16(�6�   $��31��6�   $��80��6�   G� 5   
   -   5         G�Name$ txtPassword��5      $
��Left=�6+   $��26��6F   $��113,129,0,0q�6R   $	��Top=�6g   $	��243��6   $��3��6�   $��16(�6�   $��2��6�   �� 5   
   -   5         ��Name$ txtEmail��5      $
��Left=�6$   $��26��6?   $��
113,75,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��243��6�   $��16(�6�   $��3��6�   $��1��6�   �� 5   
   -   5         ��Name$ 	btnSubmit��5      $��Submit��6$   $
��Left=�65   $��25��6P   $��	2,180,0,0q�6\   $	��Top=�6o   $��70��6�   $��1��6�   $��3��6�   �� 5   
   $��3��6   $'��!Please Enter Email and Password
��6$   $��Center=�6X   $��30��6u   $��
38,10,31,0q�6�   $	��Top=�6�   $	��297��6�   $��16(�6�   $��Center=�6�   $��Center=�6�   5      5      