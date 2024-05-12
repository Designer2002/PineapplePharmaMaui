using System.Text.Json;
using System.Text.Json.Serialization;
namespace winui_cooler;

class ConnectionManager
{
    public enum SentDataMessages
    {
        SUCCESS,
        ERROR
    }
    public enum QueryTypes
    {
        EXIT,
        LOGIN,
        REGISTER,
        SEARCH,
        GET_CART,
        ADD_TO_CART,
        GET_BY_ID,
        DELETE_FROM_CART,
        SET_CART_ITEM_COUNT
    }

    public async static Task<bool> TryLogin(string email, string password)
    {
        var data = new { QueryType = 1, Email = email, Password = password };

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        if (int.TryParse(Client.GetInstance().responseData, out int responseCode))
        {
            if (responseCode == (int)SentDataMessages.ERROR)
            {
                return false; // Ошибка входа
            }
        }
        else
        {
            // Десериализуем ответ в объект User
            var rdata = Client.GetInstance().responseData;
            var user = JsonSerializer.Deserialize<User>(rdata, new JsonSerializerOptions(){DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull});
            
            // Устанавливаем текущего пользователя
            UserManager.current = user;

            return true; // Успешный вход
        }
       
        return false;
    }

    public async static Task<bool> TryRegister(string name, string email, string password)
    {
        var data = new { QueryType = 2, Name = name, Email = email, Password = password };

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        int.TryParse(Client.GetInstance().responseData, out int responce);
        if(responce == (int)SentDataMessages.SUCCESS) return true;
        return false;
    }
    public async static Task<ShoppingCart> GetShoppingCart()
    {
        var data = new { QueryType = 4} ;

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        if (int.TryParse(Client.GetInstance().responseData, out int responseCode))
        {
            if (responseCode == (int)SentDataMessages.ERROR)
            {
                return new ShoppingCart(); // Ошибка входа
            }
        }
        else
        {
            // Десериализуем ответ в объект User
            var cart = JsonSerializer.Deserialize<ShoppingCart>(Client.GetInstance().responseData);

            return cart == null ? new ShoppingCart() : cart; // Успешный вход
        }
        return new ShoppingCart();
    }

    internal static async Task<List<Medicine>> GetCategoryList(string category)
    {
        var data = new { QueryType = 3, Category = category} ;

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        if (int.TryParse(Client.GetInstance().responseData, out int responseCode))
        {
            if (responseCode == (int)SentDataMessages.ERROR)
            {
                return new List<Medicine>();
            }
        }
        else
        {
            // Десериализуем ответ в объект User
            var found = JsonSerializer.Deserialize<List<Medicine>>(Client.GetInstance().responseData);

            return found == null ? new List<Medicine>() : found; // Успешный вход
        }
        return null;
    }

    internal static async Task<Medicine> GetItemById(int id)
    {
        var data = new { QueryType = 6, Id = id} ;

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        if (int.TryParse(Client.GetInstance().responseData, out int responseCode))
        {
            if (responseCode == (int)SentDataMessages.ERROR)
            {
                return null;
            }
        }
        else
        {
            // Десериализуем ответ в объект User
            var found = JsonSerializer.Deserialize<Medicine>(Client.GetInstance().responseData);

            return found == null ? null : found; // Успешный вход
        }
        return null;
    }

    public static async Task<bool> TryAddToCart(int id)
    {
        var data = new { QueryType = 5, Id = id };

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        int.TryParse(Client.GetInstance().responseData, out int responce);
        if(responce == (int)SentDataMessages.SUCCESS) return true;
        return false;
    }

    public static async Task<bool> TryDeleteFromCart(int id)
    {
        var data = new { QueryType = 7, Id = id };

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        int.TryParse(Client.GetInstance().responseData, out int responce);
        if(responce == (int)SentDataMessages.SUCCESS) return true;
        return false;
    }

    internal static async Task<bool> SetItemCount(int id, int count)
    {
        var data = new { QueryType = 8, Id = id, Count = count};

        // Сериализуем анонимный объект в формат JSON
        var jsonedData = JsonSerializer.Serialize(data);

        // Отправляем данные на сервер
        var task = Client.GetInstance().ConnectAsync(jsonedData);
        await task;
        int.TryParse(Client.GetInstance().responseData, out int responce);
        if(responce == (int)SentDataMessages.SUCCESS) return true;
        return false;
    }
}