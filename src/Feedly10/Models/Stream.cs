using Feedly10.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedly10.App.Dashboard
{
    public class Stream : UIModel
    {
		public Stream(Feedly.Stream streamDto) : base(streamDto.Id)
		{
			Direction = streamDto.Direction;
			Updated = streamDto.Updated;
			Title = streamDto.Title;
			Continuation = streamDto.Continuation;
			Items = streamDto.Items.Select(entry => new Entry(entry)).ToImmutableList();
		}
		public string Direction { get; private set; }
		public long Updated { get; private set; }
		public string Title { get; private set; }
		public string Continuation { get; private set; }
		public ImmutableList<Entry> Items { get; private set; }
	}
}
