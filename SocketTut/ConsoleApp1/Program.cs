using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Dynamic;
using System.Net.NetworkInformation;
using System.Security.Principal;
if (IsAdministrator())
{
    Console.WriteLine("Running as admin");
}
IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("dns.google.com");
IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
var hostName = Dns.GetHostName();
// This is the IP address of the local machine
EndPoint ipEndPoint = new IPEndPoint(ipAddress, 33345);
EndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.10"), 0);

using Socket icmpSocket = new(
    AddressFamily.InterNetwork,
    SocketType.Raw,
    ProtocolType.Icmp);
icmpSocket.Bind(new IPEndPoint(IPAddress.Any, 0));
icmpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
Stopwatch stopwatch = new Stopwatch();
using Socket udpSocket = new(
    AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp
    );
    Console.WriteLine(icmpSocket.Ttl);
        byte[] buffer = new byte[1024];
var message = "Test";
var messageBytes = Encoding.UTF8.GetBytes(message); 
icmpSocket.SendTimeout = 1000;
icmpSocket.ReceiveTimeout = 5000;

    for (short i = 1; i <= 60; i++)
    {

        udpSocket.Ttl = i;
        stopwatch.Reset();
        stopwatch.Start();
        try
        {
            udpSocket.SendTo(messageBytes, ipEndPoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

        IPAddress responseIp = null;

    try
    {

        SocketFlags socketFlags = SocketFlags.None;
        stopwatch.Stop();
        Console.WriteLine(udpSocket.Ttl);
        SocketError socketError = new SocketError();
        icmpSocket.Receive(buffer);

        Console.WriteLine(BitConverter.ToString(buffer, 0));
        string responseIP = ExtractIpFromBuffer(buffer);
        if (responseIP == ipAddress.ToString())
        {
            Console.WriteLine($"{responseIP}");
            break;
        }
    }
    catch (SocketException sx)
    {
        string responseIP = ExtractIpFromBuffer(buffer);
        Console.WriteLine(responseIP);
        Console.WriteLine($"TTL:{udpSocket.Ttl} Message:{sx.Message}");
        stopwatch.Stop();
        Console.WriteLine(icmpSocket);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"TTL:{icmpSocket.Ttl} Message:{ex.Message}");
    }
}

    icmpSocket.Shutdown(SocketShutdown.Both);
icmpSocket.Close();
udpSocket.Shutdown(SocketShutdown.Both);
udpSocket.Close();
static bool IsAdministrator()
{
    var currentIdentity = WindowsIdentity.GetCurrent();
    var currentPrincipal = new WindowsPrincipal(currentIdentity);
    return currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
}
static byte[] CreateIcmpEchoRequest()
{
    byte[] icmpPacket = new byte[32]; // 8 bytes for the ICMP header
    icmpPacket[0] = 8; 
    icmpPacket[1] = 0;
                     

    // Calculate checksum and set it in the packet (here we skip for simplicity)
    // Note: A real ICMP implementation would calculate and insert a proper checksum.
    Buffer.BlockCopy(BitConverter.GetBytes((ushort)1), 0, icmpPacket, 4, 2); // Identifier
    Buffer.BlockCopy(BitConverter.GetBytes((ushort)1), 0, icmpPacket, 6, 2); // Sequence Number

    // Compute checksum
    int checksum = 0;
    for (int i = 0; i < icmpPacket.Length; i += 2)
    {
        checksum += BitConverter.ToUInt16(icmpPacket, i);
    }
    checksum = (checksum >> 16) + (checksum & 0xffff);
    checksum += (checksum >> 16);
    ushort checksumFinal = (ushort)~checksum;
    Buffer.BlockCopy(BitConverter.GetBytes(checksumFinal), 0, icmpPacket, 2, 2);

    return icmpPacket;
}
string ExtractIpFromBuffer(byte[] buffer)
{
    if (buffer.Length >= 20)
    {
        // IP address is typically at byte positions 12-15 in IPv4 packets
        return new IPAddress(new byte[] { buffer[12], buffer[13], buffer[14], buffer[15] }).ToString();
    }
    return "Unknown IP";
}