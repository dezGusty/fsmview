   M S B A M L   `   `   `  ����  
FSMControl&     FSMControl.Windows.AddEdgeWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  AddEdgeWindow��5   	   $	��300��6   $	��400��6,   ���#3D5C5C � WindowStyle$ 
ToolWindow=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      �  � ColumnDefinitions 5	      �� 5
      $��1093* �6   �� 5      $
��250* �6    � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $	��100 �6   .+�5      �� $��Source Vertex: ��6   
�������$
��Left=�69   $��	30,30,0,0q�6T   $��1��6g   $	��Top=�6t   $	��140��6�   �� 5      $"��Target Vertex :
(StateName)��6   
�������$
��Left=�6N   $��	30,80,0,0q�6i   $��1��6|   $	��Top=�6�   $	��140��6�   �� 5      $&�� Trigger Name:
(StateAndTrigger)��6   
�������$
��Left=�6R   $��
30,130,0,0q�6m   $��1��6�   $	��Top=�6�   $	��140��6�   $��67��6�   $��2��6�   �� 5      -   5        $��sourceVeritces��5       �� DisplayMemberPath$
 Text��6G   $��170,34,40,109q�6`   $��2��6x   $��2��6�   ��6&   �� *
Vertices+6�   �� 5      -   5        $��targetVeritces��5      $
 Text��6G   $��170,84,40,55q�6`   $��2��6w   $��2��6�   ��6&   �� *
Vertices+6�   �� 5      -   5         ��Name$ 
txtTrigger��5      $
��Left=�6"   $��33��6=   $��170,134,0,0q�6I   $��1��6^    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $	��184��6�   $��2��6�   �� 5      -   5        $��Add it to the Graph��5      $
��Left=�6+   $��
122,30,0,0q�6F   $��2��6Z   $	��Top=�6g   $	��125��6   6�   5      5      