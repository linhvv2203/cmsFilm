using System;

namespace VatLid
{
	/// <summary>
	/// Summary description for updateRemote.
	/// </summary>
	public class updateRemoteItems
	{
		public updateRemoteItems()
		{
			//
			// TODO: Add constructor logic here
			//
			//string sRes= UpdateRemote.updateRemoteItems.updateItems("HINHDONG","5447","5447.gif","Khong biet",1);  
			//string sRes= UpdateRemote.updateRemoteItems.UpdateGames("192","Khong co","0","kv263","01",0);
		}
		private static string updateAll(string Content)
		{
			VatLid.updateWS rem = new VatLid.updateWS();
			string sRes="-1";
			try
			{
				sRes= rem.InsertUMT("vtmedia","tvpl123#$",Content);
			}
			catch (Exception ex)
			{
				sRes= ex.Message; 
			}
			finally
			{
				rem.Dispose();
			}
			return sRes ;
		}
		/// <summary>
		/// Ham nay dung cap nhat ma games vao he thong
		/// </summary>
		/// <param name="ItemCode">Ma Games</param>
		/// <param name="ItemName">Ten Game</param>
		/// <param name="ItemDesc">0:qua WS,1:WP</param>
		/// <param name="ItemType">Ma Game cua CP</param>
		/// <param name="ItemFileName">CP Code</param>
		/// <param name="ItemStatus">2:Enabled,1:Deleting,0:Deleted</param>
		/// <returns></returns>
		public static string UpdateGames(string ItemCode,string ItemName,string ItemDesc , string ItemType,string ItemFileName,int ItemStatus,int id)
		{
			string SQL=string.Format("{0} '{1}','{2}','{3}','{4}','{5}',{6},{7}",
				"Exec dbo.insert_Item_Games",ItemCode,ItemName,ItemDesc,ItemType,ItemFileName,ItemStatus,id) ;
			return updateAll(SQL);
		}
		private static string UpdateItems(string ContentType, string ItemCode,string ItemFileName,string ItemDesc,int ItemStatus,int id)
		{
			string SQL=string.Format("{0} '{1}','{2}','{3}','{4}','{5}',{6},{7}",
				"Exec dbo.insert_Item_Images",ContentType,ItemCode,ItemFileName,ItemDesc,"",ItemStatus,id) ;
			return updateAll(SQL);
		}
		/// <summary>
		/// Ham nay dung cap nhat ma anh,am nhac,mp3,video vao he thong
		/// </summary>
		/// <param name="type">HINHTINH/STATIC,HINHDONG/DYNAMIC,TRUETONE/AMTHUC,POLYTONE,MP3,VIDEO</param>
		/// <param name="ItemCode">Ma khai bao</param>
		/// <param name="ItemFileName">Ten file</param>
		/// <param name="ItemDesc">Ten item</param>
		/// <param name="ItemStatus">2:Enabled,1:Deleting,0:Deleted</param>
		/// <returns></returns>
		public static string updateItems(string type,string ItemCode,string ItemFileName,string ItemDesc,int ItemStatus,int id)
		{
			string ContentType="0" ;
			switch(type)
			{
				case "HINHTINH":ContentType="4";break ;
				case "STATIC":ContentType="7";break ;
				case "HINHDONG":ContentType="5";break ;
				case "DYNAMIC":ContentType="8";break ;
				case "TRUETONE":ContentType="1";break ;
				case "AMTHUC":ContentType="3";break ;
				case "POLYTONE":ContentType="2";break ;
				case "MP3":ContentType="6";break ;
				case "VIDEO":ContentType="9";break ;
				default: ContentType="0";break ;
			}
			return UpdateItems(ContentType,ItemCode,ItemFileName,ItemDesc,ItemStatus,id);
		}
	}
}
