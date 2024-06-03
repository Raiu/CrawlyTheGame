namespace Crawly;

public class ConfigManager
{
    //private  _model;

    public ConfigManager()
    {
        LoadConfig();
    }

    public ConfigManager(string configPath)
    {
        //  _model = Toml.ToModel(configPath);
        LoadConfig();
    }

    private void LoadConfig()
    {
        //_model = Toml.ReadFile("config.toml");
    }
}
