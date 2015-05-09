using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using Voice;
using MetroFramework.Forms;
using System.Net;

namespace blop
{
    public partial class CallChat : MetroForm
    {
        private Socket s;
        private Thread t;
        private bool connected = false;

        public CallChat()
        {
            InitializeComponent();

            s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            t = new Thread(new ThreadStart(Voice_In));
            t.IsBackground = true;
        }


        private void Voice_In()
        {
            byte[] br;
            s.Bind(new IPEndPoint(IPAddress.Any, 0)); //int.Parse(textBox2.Text)
            while (true)
            {
                br = new byte[16384];
                s.Receive(br);
                m_Fifo.Write(br, 0, br.Length);
            }
        }

        private void Voice_Out(IntPtr data, int size)
        {
            //for Recorder
            if (m_RecBuffer == null || m_RecBuffer.Length < size)
                m_RecBuffer = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(data, m_RecBuffer, 0, size);
            //Microphone ==> data ==> m_RecBuffer ==> m_Fifo
            s.SendTo(m_RecBuffer, new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox3.Text)));
        }


        private WaveOutPlayer m_Player;
        private WaveInRecorder m_Recorder;
        private FifoStream m_Fifo = new FifoStream();

        private byte[] m_PlayBuffer;
        private byte[] m_RecBuffer;

        private void Start()
        {
            Stop();
            try
            {
                WaveFormat fmt = new WaveFormat(44100, 16, 2);
                m_Player = new WaveOutPlayer(-1, fmt, 16384, 3, new BufferFillEventHandler(Filler));
                m_Recorder = new WaveInRecorder(-1, fmt, 16384, 3, new BufferDoneEventHandler(Voice_Out));
            }
            catch
            {
                Stop();
                throw;
            }
        }

        private void Stop()
        {
            if (m_Player != null)
                try
                {
                    m_Player.Dispose();
                }
                finally
                {
                    m_Player = null;
                }
            if (m_Recorder != null)
                try
                {
                    m_Recorder.Dispose();
                }
                finally
                {
                    m_Recorder = null;
                }
            m_Fifo.Flush(); // clear all pending data
        }

        private void Filler(IntPtr data, int size)
        {
            if (m_PlayBuffer == null || m_PlayBuffer.Length < size)
                m_PlayBuffer = new byte[size];
            if (m_Fifo.Length >= size)
                m_Fifo.Read(m_PlayBuffer, 0, size);
            else
                for (int i = 0; i < m_PlayBuffer.Length; i++)
                    m_PlayBuffer[i] = 0;
            System.Runtime.InteropServices.Marshal.Copy(m_PlayBuffer, 0, data, size);
            // m_Fifo ==> m_PlayBuffer ==> data ==> Speakers
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (connected == false)
            {
                t.Start();
                connected = true;
            }
            Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void CallChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Abort();
            s.Close();
            Stop();
        }
    }
}

