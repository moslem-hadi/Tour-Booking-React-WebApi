import React, { Component } from "react";
import { Link, Redirect } from "react-router-dom";
import { Loader } from "../Layout/Common/Loader";
import { getPage } from "../../services/pageService";

export default class PagePage extends Component {
  state = { pageInfo: null, isLoaded: false, error: null, slug: null };

  componentDidUpdate(prevProps) {
    if (prevProps.match.params.slug !== this.props.match.params.slug) {
      this.setState({ ...this.state, slug: this.props.match.params.slug });
    }
  }
  async componentDidMount() {
    let slug = this.props.match.params.slug;
    document.title = "پرهان ترانسفر";

    try {
      const { data: page } = await getPage(slug);
      this.setState({
        isLoaded: true,
        pageInfo: page.success ? page.data : null,
        slug: slug
      });
      if (page.data && page.data.title) document.title = page.data.title;
    } catch (ex) {
      //window.location = "/404";
      this.setState({
        isLoaded: true,
        error: true
      });
    }
  }
  render() {
    let { isLoaded, pageInfo, error } = this.state;
    return !isLoaded ? (
      <section className="py-6">
        <div className="container">
          <Loader />
        </div>
      </section>
    ) : pageInfo == null || error === true ? (
      <Redirect to="/404" />
    ) : (
      <div>
        <section className="hero py-4 py-lg-5">
          <div className="container position-relative">
            <ol className="breadcrumb pl-0  justify-content-center">
              <li className="breadcrumb-item">
                <Link to="/">صفحه نخست</Link>
              </li>
              <li className="breadcrumb-item active">{pageInfo.title}</li>
            </ol>
            <h1 className="hero-heading mb-5">{pageInfo.title}</h1>
          </div>
        </section>
        <section className="py-6">
          <div className="container">
            <div
              dangerouslySetInnerHTML={{
                __html: pageInfo.text.replace(/(<? *script)/gi, "illegalscript")
              }}
            ></div>
          </div>
        </section>
      </div>
    );
  }
}
