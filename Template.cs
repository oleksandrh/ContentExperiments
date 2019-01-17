public class Template
{
    public int Id { get; set; }
    public Feature[] Features { get; set; }

    public Template()
    {
        Id = 1;
        Features = new[]
        {
            new Feature()
            {
                Id = 1,
                FeatureType = new FeatureType() { Id = 1, Name = "Header", IsContainer = false },
                PropertiesSet = new PropertiesSet
                {
                    Id = 1,
                    Text = "Some text for header",
                    BackgroundColor = "#ffffff",
                    Font = "Arial, Helvetica, sans-serif",
                    ChildFeaturesAlign = "vertical"
                },
               Features = null
            },
            new Feature()
            {
                Id = 2,
                FeatureType = new FeatureType() { Id = 2, Name = "Products", IsContainer = true },
                PropertiesSet = new PropertiesSet
                {
                    Id = 2,
                    ChildFeaturesAlign = "vertical"
                },
                Features = new Feature[]
                {
                    new Feature()
                    {
                        Id = 3,
                        FeatureType = new FeatureType() { Id = 3, Name = "Product", IsContainer = false },
                        PropertiesSet = new PropertiesSet
                        {
                            Id = 3,
                            Text = "Some product name"
                        },
                        Features = null
                    }
                }
            }
        };
    }
}

public class Feature
{
    public int Id { get; set; }
    public FeatureType FeatureType { get; set; }
    public PropertiesSet PropertiesSet { get; set; }
    public Feature[] Features { get; set; }
}

public class FeatureType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsContainer { get; set; }
}

public class PropertiesSet
{
    public int Id { get; set; }
    public string BackgroundColor { get; set; }
    public string Text { get; set; }
    public string Font { get; set; }
    public string ChildFeaturesAlign { get; set; }
}
