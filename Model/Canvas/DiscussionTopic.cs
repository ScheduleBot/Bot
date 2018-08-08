using System;
using System.Collections.Generic;

namespace SimpleEchoBot.Model
{
    public class DiscussionTopic
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime last_reply_at { get; set; }
        public object delayed_post_at { get; set; }
        public object posted_at { get; set; }
        public int assignment_id { get; set; }
        public object root_topic_id { get; set; }
        public object position { get; set; }
        public bool podcast_has_student_posts { get; set; }
        public string discussion_type { get; set; }
        public object lock_at { get; set; }
        public bool allow_rating { get; set; }
        public bool only_graders_can_rate { get; set; }
        public bool sort_by_rating { get; set; }
        public bool is_section_specific { get; set; }
        public object user_name { get; set; }
        public int discussion_subentry_count { get; set; }
        public Permissions permissions { get; set; }
        public bool? require_initial_post { get; set; }
        public bool user_can_see_posts { get; set; }
        public object podcast_url { get; set; }
        public string read_state { get; set; }
        public int unread_count { get; set; }
        public bool subscribed { get; set; }
        public List<object> topic_children { get; set; }
        public List<object> group_topic_children { get; set; }
        public List<object> attachments { get; set; }
        public bool published { get; set; }
        public bool can_unpublish { get; set; }
        public bool locked { get; set; }
        public bool can_lock { get; set; }
        public bool comments_disabled { get; set; }
        public Author author { get; set; }
        public string html_url { get; set; }
        public string url { get; set; }
        public bool pinned { get; set; }
        public object group_category_id { get; set; }
        public bool can_group { get; set; }
        public bool locked_for_user { get; set; }
        public string message { get; set; }
        public object todo_date { get; set; }
    }
}