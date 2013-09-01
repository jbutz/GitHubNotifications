using System;
using RestSharp;
using System.Collections.Generic;

namespace GitHubNotifications
{
	public class GitHubApi
	{
		private RestClient _client;
		private string _auth_token;
		//private string _client_id;
		//private string _client_secret;
		private string _last_modified;

		private DateTime _last_poll;
		private int _poll_interval;

		public GitHubApi (string client_id, string client_secret)
		{
			//_client_id = client_id;
			//_client_secret = client_secret;
			_client = new RestClient ("https://api.github.com");
		}

		public void SetAuthenticationToken(string token)
		{
			_auth_token = token;
		}

		public List<ghNotification> GetNotifications()
		{
			// Make sure we aren't polling too often
			if(DateTime.Now > _last_poll.AddSeconds(_poll_interval))
				return new List<ghNotification>();

			var request = new RestRequest ("/notifications");
			request.AddHeader ("Authorization", "token " + _auth_token);

			if (_last_modified != null)
				request.AddHeader ("If-Modified-Since", _last_modified);

			//var r = _client.Execute(request);

			var r = _client.Execute<List<ghNotification>>(request);
			_last_poll = DateTime.Now;

			foreach (Parameter h in r.Headers)
			{
				if (h.Name == "Last-Modified")
					_last_modified = h.Value.ToString();
				else if (h.Name == "X-Poll-Interval")
					_poll_interval = Convert.ToInt32(h.Value);
			}

			if(r.StatusCode == System.Net.HttpStatusCode.NotModified)
				return new List<ghNotification>();

			//_last_modified = r.Headers ["Last-Modified"].Value.ToString();
			//_poll_interval = Convert.ToInt32 (r.Headers ["X-Poll-Interval"].Value.ToString ());
			//Console.WriteLine (r.Content);

			return r.Data;
		}

		public ghComment GetCommentData(string url)
		{
			var request = new RestRequest (new Uri (url));
			request.AddHeader ("Authorization", "token " + _auth_token);

			var r = _client.Execute<ghComment> (request);

			return r.Data;
		}

		public void MarkThreadRead(string url)
		{
			var request = new RestRequest (new Uri (url), Method.PATCH);
			request.AddHeader ("Authorization", "token " + _auth_token);

			_client.Execute (request);
		}

	}

	public class ghNotification
	{
		public int id { get; set; }
		public ghRepo repository { get; set; }
		public ghSubject subject { get; set; }
		public string reason { get; set; }
		public bool unread { get; set; }
		public DateTime updated_at { get; set; }
		public DateTime last_read_at { get; set; }
		public string url { get; set; }

	}

	public class ghRepo
	{
		public int id { get; set; }
		public string name { get; set; }
		public string full_name { get; set; }
		public string description { get; set; }
		public bool Private { get; set; }
		public bool fork { get; set; }
		public string url { get; set; }
		public string html_url { get; set; }
		public ghRepoOwner owner { get; set; }
	}
	public class ghRepoOwner
	{
		public string login { get; set; }
		public int id { get; set; }
		public string avatar_url { get; set; }
		public string gravatar_id { get; set; }
		public string url { get; set; }
	}

	public class ghSubject
	{
		public string title { get; set; }
		public string url { get; set; }
		public string latest_comment_url { get; set; }
		public string type { get; set; }
	}

	public class ghComment
	{
		public string url { get; set; }
		public string html_url { get; set; }
		public int id { get; set; }
	}


}

