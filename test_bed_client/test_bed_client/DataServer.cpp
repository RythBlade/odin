#include "DataServer.h"

DataServer::DataServer()
{
    ClientSocket = INVALID_SOCKET;
}


DataServer::~DataServer()
{
}

int DataServer::initialiseServer()
{
    WSADATA wsaData;
    int iResult;

    SOCKET ListenSocket = INVALID_SOCKET;

    struct addrinfo *result = NULL;
    struct addrinfo hints;

    // Initialize Winsock
    iResult = WSAStartup( MAKEWORD( 2, 2 ), &wsaData );
    if ( iResult != 0 )
    {
        printf( "WSAStartup failed with error: %d\n", iResult );
        return 1;
    }

    ZeroMemory( &hints, sizeof( hints ) );
    hints.ai_family = AF_INET;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;
    hints.ai_flags = AI_PASSIVE;

    // Resolve the server address and port
    iResult = getaddrinfo( NULL, DEFAULT_PORT, &hints, &result );
    if ( iResult != 0 )
    {
        printf( "getaddrinfo failed with error: %d\n", iResult );
        WSACleanup();
        return 1;
    }

    // Create a SOCKET for connecting to server
    ListenSocket = socket( result->ai_family, result->ai_socktype, result->ai_protocol );
    if ( ListenSocket == INVALID_SOCKET )
    {
        printf( "socket failed with error: %ld\n", WSAGetLastError() );
        freeaddrinfo( result );
        WSACleanup();
        return 1;
    }

    // Setup the TCP listening socket
    iResult = bind( ListenSocket, result->ai_addr, ( int ) result->ai_addrlen );
    if ( iResult == SOCKET_ERROR )
    {
        printf( "bind failed with error: %d\n", WSAGetLastError() );
        freeaddrinfo( result );
        closesocket( ListenSocket );
        WSACleanup();
        return 1;
    }

    freeaddrinfo( result );

    // mark the socket as non-blocking
    u_long data = 1;
    iResult = ioctlsocket( ListenSocket, FIONBIO, &data );

    iResult = listen( ListenSocket, SOMAXCONN );
    if ( iResult == SOCKET_ERROR )
    {
        printf( "listen failed with error: %d\n", WSAGetLastError() );
        closesocket( ListenSocket );
        WSACleanup();
        return 1;
    }

    // Accept/wait for a client socket
    ClientSocket = accept( ListenSocket, NULL, NULL );

    while ( ClientSocket == INVALID_SOCKET )
    {
        if ( WSAGetLastError() != WSAEWOULDBLOCK )
        {
            printf( "accept failed with error: %d\n", WSAGetLastError() );
            closesocket( ListenSocket );
            WSACleanup();
            return 1;
        }
        else
        {
            printf( "Waiting for connection. \n" );
            Sleep( 1000 );

            ClientSocket = accept( ListenSocket, NULL, NULL );
        }
    }

    // No longer need server socket
    closesocket( ListenSocket );

    return 0;
}

int DataServer::closeServer()
{
    // shutdown the connection since we're done
    int iResult = shutdown( ClientSocket, SD_SEND );
    if ( iResult == SOCKET_ERROR )
    {
        printf( "shutdown failed with error: %d\n", WSAGetLastError() );
        closesocket( ClientSocket );
        WSACleanup();
        return 1;
    }

    // cleanup
    closesocket( ClientSocket );
    WSACleanup();
    return 0;
}

int DataServer::sendData( char* dataToSend, unsigned int lengthOfData )
{
    // Echo the buffer back to the sender
    int iSendResult = send( ClientSocket, dataToSend, lengthOfData, 0 );
    if ( iSendResult == SOCKET_ERROR )
    {
        int returnedError = WSAGetLastError();

        if ( returnedError != WSAEWOULDBLOCK )
        {
            printf( "send failed with error: %d\n", WSAGetLastError() );
            closesocket( ClientSocket );
            WSACleanup();
            return 1;
        }
    }
    //printf( "Bytes sent: %d\n", iSendResult );
    return 0;
}

