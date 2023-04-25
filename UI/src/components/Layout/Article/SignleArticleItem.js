import React from "react";
import { Link } from "react-router-dom";

const SignleArticleItem = ({ article }) => {
  return (
    <div className="article mb-4">
      <Link
        to={`/blog/${article.slug}`}
        className="d-flex align-items-center justify-content-start  text-decoration-none"
      >
        <img src={article.img} alt={article.title} className="ml-3 shadow" />
        <div>
          <h6 className="text-dark">{article.title}</h6>
          <span className="mt-2 d-block text-muted text-sm">
            <i className="fa fa-calendar ml-1"></i> {article.date}
          </span>
        </div>
      </Link>
    </div>
  );
};

export default SignleArticleItem;
