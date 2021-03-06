﻿namespace PingerManager.Config
{
    public class ConfigEntity
    {
        public string Host { get; set; }
        public int Period { get; set; }
        public Protocol Protocol { get; set; }
        public int Port { get; set; }
        public int ValidStatusCode { get; set; }
    }

    public enum Protocol
    {
        Icmp = 1,
        Tcp,
        Http
    }
}
