using System.Collections.Generic;

namespace Feedly10.App.Feedly
{
    public class Stream
    {
        public string Id { get; set; }
        public string Direction { get; set; }
        public long Updated { get; set; }
        public string Title { get; set; }
        public List<Alternate> Alternate { get; set; }
        public string Continuation { get; set; }
        public List<Entry> Items { get; set; }
    }

    public class Alternate
    {
        public string Href { get; set; }
        public string Type { get; set; }
    }

    public class Origin
    {
        public string StreamId { get; set; }
        public string Title { get; set; }
        public string HtmlUrl { get; set; }
    }

    public class Summary
    {
        public string Direction { get; set; }
        public string Content { get; set; }
    }

    public class Enclosure
    {
        public string Href { get; set; }
    }

    public class ContentDto
    {
        public string Direction { get; set; }
        public string Content { get; set; }
    }

    public class Visual
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentType { get; set; }
    }

    public class Entry
    {
        public string OriginId { get; set; }
        public string Fingerprint { get; set; }
        public string Id { get; set; }
        public long Updated { get; set; }
        public string Title { get; set; }
        public long Published { get; set; }
        public long Crawled { get; set; }
        public List<Alternate> Alternate { get; set; }
        public string Author { get; set; }
        public Origin Origin { get; set; }
        public Summary Summary { get; set; }
        public List<Enclosure> Enclosure { get; set; }
        public ContentDto Content { get; set; }
        public Visual Visual { get; set; }
        public bool Unread { get; set; }
    }
}