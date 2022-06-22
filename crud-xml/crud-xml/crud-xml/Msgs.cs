using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace crud_xml
{
    

    public class Msgs
    {
        public static string _tituloAlerta = "Êitha! Perece que algo não esta certo.";
        public static string _tituloErro = "Errado! Refaça o processo.";
        public static string _tituloSucesso = "Muito bem!";

        public static void alertaAlerta(string msg)
        {
            MessageBox.Show(msg, _tituloAlerta, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void alertaErro(string msg)
        {
            MessageBox.Show(msg, _tituloErro, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void alertaSucesso(string msg)
        {
            MessageBox.Show(msg, _tituloSucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void alertaBox(string msg)
        {
            MessageBox.Show(msg);
        }

        public string TituloAlerta
        {
            set { _tituloAlerta = "Êitha! Algo deu errado."; }
            get { return _tituloAlerta;  }
        }

        public string TituloErro
        {
            set { _tituloErro = "ERRO! Veririfique e tente novamente."; }
            get { return _tituloErro; }
        }
        public string TituloSucesso
        {
            get { return _tituloSucesso; }
            set { _tituloSucesso = "Sucesso! Tudo certo"; }
        }
    }
}
