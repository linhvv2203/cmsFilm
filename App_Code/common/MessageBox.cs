using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Text;


namespace VatLid
{
	public class MessageBox
	{
		private static Hashtable m_executingPages = new Hashtable();

		private MessageBox(){}

        /// <summary>
        /// Close child window
        /// </summary>
        public static void Close()
        {
            Show("window.close();");
        }

        public static void Close(string sMessage)
        {
            Show(@"alert( """ + sMessage + @""" );window.close();");
        }

		public static void Show( string sMessage )
		{
			// If this is the first time a page has called this method then
			if( !m_executingPages.Contains( HttpContext.Current.Handler ) )
			{
				// Attempt to cast HttpHandler as a Page.
				Page executingPage = HttpContext.Current.Handler as Page;

				if( executingPage != null )
				{
					// Create a Queue to hold one or more ItemGames.
					Queue messageQueue = new Queue();

					// Add our message to the Queue
					messageQueue.Enqueue( sMessage );

					// Add our message queue to the hash table. Use our page reference
					// (IHttpHandler) as the key.
					m_executingPages.Add( HttpContext.Current.Handler, messageQueue );

					// Wire up Unload event so that we can inject 
					// some JavaScript for the alerts.
					executingPage.Unload += new EventHandler( ExecutingPage_Unload );
				}   
			}
			else
			{
				// If were here then the method has allready been 
				// called from the executing Page.
				// We have allready created a message queue and stored a
				// reference to it in our hastable. 
				Queue queue = (Queue) m_executingPages[ HttpContext.Current.Handler ];

				// Add our message to the Queue
				queue.Enqueue( sMessage );
			}
		}

        /// <summary>
        /// Show messge
        /// </summary>
        /// <param name="sMessage"></param>
        public static void ShowMessages(string sMessage)
        {
            sMessage = sMessage.Replace("\n", "\\n");
            sMessage = sMessage.Replace("\"", "'");
            sMessage = @"alert( """ + sMessage + @""" );";
            Show(sMessage);
        }

        public static void ShowWindow(string url)
        {
            string param = "window.open(" + url + ");";
            Show(param);
        }

		// Our page has finished rendering so lets output the
		// JavaScript to produce the alert's
		private static void ExecutingPage_Unload(object sender, EventArgs e)
		{
			// Get our message queue from the hashtable
			Queue queue = (Queue) m_executingPages[ HttpContext.Current.Handler ];

			if( queue != null )
			{
				StringBuilder sb = new StringBuilder();

				// How many ItemGames have been registered?
				int iMsgCount = queue.Count;

				// Use StringBuilder to build up our client slide JavaScript.
				sb.Append( "<script language='javascript'>" );

				// Loop round registered ItemGames
				string sMsg;
				while( iMsgCount-- > 0 )
				{
					sMsg = (string) queue.Dequeue();
					sMsg = sMsg.Replace( "\n", "\\n" );
					sMsg = sMsg.Replace( "\"", "'" );
					sb.Append( @"alert( """ + sMsg + @""" );" );
				}

				// Close our JS
				sb.Append( @"</script>" );

				// Were done, so remove our page reference from the hashtable
				m_executingPages.Remove( HttpContext.Current.Handler );

				// Write the JavaScript to the end of the response stream.
				HttpContext.Current.Response.Write( sb.ToString() );
			}
		}
	}
}
