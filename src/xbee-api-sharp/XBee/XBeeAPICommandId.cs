﻿namespace XBee
{
    public enum XBeeAPICommandId
    {
        REQUEST_64 = 0x00,
        REQUEST_16 = 0x01,
        AT_COMMAND_REQUEST = 0x08,
        AT_COMMAND_QUEUE_REQUEST = 0x09,   
        TRANSMIT_DATA_REQUEST = 0x10,
        EXPLICIT_ADDR_REQUEST = 0x11,
        REMOTE_AT_COMMAND_REQUEST = 0x17,
        CREATE_SOURCE_ROUTE = 0x21,
        RECEIVE_64_RESPONSE = 0x80,
        RECEIVE_16_RESPONSE = 0x81,
        RECEIVE_64_IO_RESPONSE = 0x82,
        RECEIVE_16_IO_RESPONSE = 0x83,    
        AT_COMMAND_RESPONSE = 0x88,    
        TX_STATUS_RESPONSE = 0x89,    
        MODEM_STATUS_RESPONSE = 0x8A,
        TRANSMIT_STATUS_RESPONSE = 0x8B,
        RECEIVE_PACKET_RESPONSE = 0x90,    
        EXPLICIT_RX_INDICATOR_RESPONSE = 0x91,
        IO_SAMPLE_RESPONSE = 0x92,
        SENSOR_READ_INDICATOR = 0x94,
        NODE_IDENTIFIER_RESPONSE = 0x95,
        REMOTE_AT_COMMAND_RESPONSE = 0x97,     
        FIRMWARE_UPDATE_STATUS = 0xA0,
        ROUTE_RECORD_INDICATOR = 0xA1,
        MANYTOONE_ROUTE_REQUEST_INDICATOR = 0xA3,
        UNKNOWN = 0xFF,
    }
}
