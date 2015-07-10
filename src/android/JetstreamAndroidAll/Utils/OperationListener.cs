namespace JetstreamAndroid.Utils
{
  interface IOperationListener
  {
    void OnOperationStarted();

    void OnOperationFinished();

    void OnOperationFailed();
  }
}