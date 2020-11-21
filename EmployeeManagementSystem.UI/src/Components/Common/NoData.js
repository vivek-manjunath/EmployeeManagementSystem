import React from "react";
import { Segment, Header, Icon } from "semantic-ui-react";

const NoData = () => {
  return (
    <Segment placeholder>
      <Header icon>
        <Icon name="search" />
        No data
      </Header>
      <Segment.Inline>
        {/* <Button as={Link} to="/activities" primary>
          Return to Activities page
        </Button> */}
      </Segment.Inline>
    </Segment>
  );
};

export default NoData;
