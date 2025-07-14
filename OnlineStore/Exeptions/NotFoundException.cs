namespace OnlineStore.Exeptions;

public class NotFoundException(string message) : Exception(message)
{ 
}