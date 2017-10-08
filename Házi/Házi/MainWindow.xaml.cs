﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Házi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        
        Stack<string> seged = new Stack<string>();
        public string szinek()
        {
            int szin = Convert.ToInt32(talalat.Content);
            while (szin > 0)
            {
                if (szin % 16 < 10)
                {
                    seged.Push((szin % 16).ToString());
                }
                else if (szin % 16 > 9)
                {
                    seged.Push(Convert.ToString(Convert.ToChar(55 + szin % 16)));
                }

                szin = szin / 16;
            }
            seged.Push("#");
            for (int i = seged.Count; i > 0; i--)
            {
                szin +=Convert.ToInt32(seged.Pop());
                
            }
            return szin.ToString();
        }
        static Stack<bool> mentes = new Stack<bool>();
        static int lenyomott = 0;
        static double alsohatar = 0;
        static double felsohatar = 10000;
        static double kozepso = (alsohatar + felsohatar) / 2;
        private void kisebb_Click(object sender, RoutedEventArgs e)
        {
            felsohatar = kozepso;
            kozepso = (alsohatar + felsohatar) / 2;
            talalat.Content = Math.Round(kozepso,MidpointRounding.ToEven).ToString();
            lenyomott++;
            mentes.Push(true);
            talalat.Content = szinek();

            undo.IsEnabled = true;
        }

        private void nagyobb_Click(object sender, RoutedEventArgs e)
        {
            alsohatar = kozepso;
            kozepso = (alsohatar + felsohatar) / 2;
            talalat.Content = Math.Round(kozepso,0,MidpointRounding.ToEven).ToString();
            lenyomott++;
            mentes.Push(false); 


            undo.IsEnabled = true;
        }

        private void egyenlő_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gratulálok! Nyertél! Lenyomott gomobok száma: "+lenyomott , "Nyertél!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void undo_Click(object sender, RoutedEventArgs e)
        {
            if (mentes.Peek())
            {
                kozepso = felsohatar;
                felsohatar = kozepso*2 - alsohatar ;
                talalat.Content = Math.Round(kozepso).ToString();
                lenyomott--;
                mentes.Pop();
            }
            else if (!mentes.Peek())
            {
                kozepso = alsohatar;
                alsohatar = alsohatar*2-felsohatar;
                talalat.Content = Math.Round(kozepso,0,MidpointRounding.ToEven).ToString();
                lenyomott--;
                mentes.Pop();
            }

            if (Math.Round(kozepso) == 5000)
            {
                undo.IsEnabled = false;
            }
        }
    }
}
