// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Anonymous
{
    using System.IO;

    /// <summary>
    /// 提供對匿名檔案的部分操作支援
    /// </summary>
    public sealed class AnonymousFile
    {
        #region Constructors

        /// <summary>
        /// 初始化 <see cref="AnonymousFile"/> 類別的新執行個體
        /// </summary>
        /// <param name="directory">目錄路徑</param>
        public AnonymousFile(string directory)
        {
            Directory.CreateDirectory(directory);
            this._path = directory + "/{0}";
        }

        #endregion Constructors

        #region Fields

        private readonly string _path;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 建立或覆寫匿名檔案
        /// </summary>
        /// <param name="id">識別碼</param>
        /// <returns>檔案流</returns>
        public FileStream Create(int id)
        {
            return File.Create(this.CombinePath(id));
        }

        /// <summary>
        /// 刪除匿名檔案
        /// </summary>
        /// <param name="id">識別碼</param>
        public void Delete(int id)
        {
            var name = this.CombinePath(id);
            if (File.Exists(name))
                File.Delete(name);
        }

        /// <summary>
        /// 取回匿名檔案
        /// </summary>
        /// <param name="id">識別碼</param>
        /// <param name="file">檔案流</param>
        /// <param name="mode">檔案流的開啟方式</param>
        /// <param name="access">檔案流的存取權限</param>
        /// <param name="share">檔案流的分享方式</param>
        /// <returns>如果檔案存在則回傳 <see langword="true"/> ，反之則為 <see langword="false"/></returns>
        public bool Retrieve(int id, out FileStream file, FileMode mode = FileMode.OpenOrCreate, FileAccess access = FileAccess.ReadWrite, FileShare share = FileShare.ReadWrite)
        {
            file = default;
            var name = this.CombinePath(id);
            var fi = new FileInfo(name);
            if (fi.Exists)
            {
                file = fi.Open(mode, access, share);
            }

            return fi.Exists;
        }

        private string CombinePath(int id)
        {
            return string.Format(this._path, AnonymousCore.Convert(id));
        }

        #endregion Methods
    }
}