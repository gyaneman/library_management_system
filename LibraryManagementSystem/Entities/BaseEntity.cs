using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Entities
{
    class BaseEntity
    {
        enum State
        {
            New,        // 新しいオブジェクト
            Recorded,   // DBから取得・DBに保存してから、まだ変更されていない状態
            Edited      // 変更されてからまだ保存されていない状態
        }

        State state = State.New;
        private string id;
        private string created_at;
        private string edited_at;

        public string Id
        {
            set { }
            get
            {
                return id;
            }
        }

        public string CreatedAt
        {
            set { }
            get
            {
                return this.created_at;
            }
        }

        public string EditedAt
        {
            set { }
            get
            {
                return this.edited_at;
            }
        }

        public BaseEntity()
        {
            this.state = State.New;
        }

        public BaseEntity(string id)
        {
            this.id = id;
            this.state = State.Recorded;
        }

        protected void UpdateEditedAt()
        {
            state = State.Edited;
        }

        protected void Save()
        {
            state = State.Recorded;
        }

    }
}
