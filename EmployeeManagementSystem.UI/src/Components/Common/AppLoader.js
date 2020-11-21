import React from "react";
import { Loader, Dimmer, Segment } from "semantic-ui-react";

const AppLoader = () => {
  return (
      <Dimmer active inverted>
        <Loader inline="centered" size="huge"/>
      </Dimmer>
  );
};

export default AppLoader;
