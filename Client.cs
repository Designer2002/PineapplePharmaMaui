using System.Net.Sockets;
using System.Text;

class Client
{
    private static Client instance;
    public static Client GetInstance()
    {
        if (instance == null)   
            instance = new Client();
        return instance;
    }
    public string responseData { get; set; }
    public async Task ConnectAsync(string message)
    {
        try
        {
            Int32 port = 13000;
            TcpClient client = new TcpClient("127.0.0.1", port);

            Byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();

            // Send the message asynchronously
            await stream.WriteAsync(data, 0, data.Length);

            // Receive the response asynchronously
            data = new Byte[102400];
            Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
            responseData = Encoding.UTF8.GetString(data, 0, bytes);

            // Close the stream and client
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }


    }

}