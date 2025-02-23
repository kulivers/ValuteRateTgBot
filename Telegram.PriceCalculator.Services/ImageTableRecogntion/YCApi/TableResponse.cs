namespace YandexCloudOCR;

public class TableResponse
{
    public RecRes result { get; set; }
}

public class RecRes
{
    public TextAnnotation textAnnotation { get; set; }
    public string page { get; set; }
}

public class TextAnnotation
{
    public string width { get; set; }
    public string height { get; set; }
    public Blocks[] blocks { get; set; }
    public object[] entities { get; set; }
    public Table[] tables { get; set; }
    public string fullText { get; set; }
    public string rotate { get; set; }
}

public class Blocks
{
    public BoundingBox boundingBox { get; set; }
    public Lines[] lines { get; set; }
    public Languages[] languages { get; set; }
    public TextSegments[] textSegments { get; set; }
}

public class BoundingBox
{
    public Vertices[] vertices { get; set; }
}

public class Vertices
{
    public string x { get; set; }
    public string y { get; set; }
}

public class Lines
{
    public BoundingBox1 boundingBox { get; set; }
    public string text { get; set; }
    public Words[] words { get; set; }
    public TextSegments1[] textSegments { get; set; }
    public string orientation { get; set; }
}

public class BoundingBox1
{
    public Vertices1[] vertices { get; set; }
}

public class Vertices1
{
    public string x { get; set; }
    public string y { get; set; }
}

public class Words
{
    public BoundingBox2 boundingBox { get; set; }
    public string text { get; set; }
    public string entityIndex { get; set; }
    public TextSegments2[] textSegments { get; set; }
}

public class BoundingBox2
{
    public Vertices2[] vertices { get; set; }
}

public class Vertices2
{
    public string x { get; set; }
    public string y { get; set; }
}

public class TextSegments2
{
    public string startIndex { get; set; }
    public string length { get; set; }
}

public class TextSegments1
{
    public string startIndex { get; set; }
    public string length { get; set; }
}

public class Languages
{
    public string languageCode { get; set; }
}

public class TextSegments
{
    public string startIndex { get; set; }
    public string length { get; set; }
}

public class Table
{
    public BoundingBox3 boundingBox { get; set; }
    public string rowCount { get; set; }
    public string columnCount { get; set; }
    public Cell[] cells { get; set; }
}

public class BoundingBox3
{
    public Vertices3[] vertices { get; set; }
}

public class Vertices3
{
    public string x { get; set; }
    public string y { get; set; }
}

public class Cell
{
    public BoundingBox4 boundingBox { get; set; }
    public string rowIndex { get; set; }
    public string columnIndex { get; set; }
    public string columnSpan { get; set; }
    public string rowSpan { get; set; }
    public string text { get; set; }
    public TextSegments3[] textSegments { get; set; }
}

public class BoundingBox4
{
    public Vertices4[] vertices { get; set; }
}

public class Vertices4
{
    public string x { get; set; }
    public string y { get; set; }
}

public class TextSegments3
{
    public string startIndex { get; set; }
    public string length { get; set; }
}

