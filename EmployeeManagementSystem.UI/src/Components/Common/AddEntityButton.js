import React from "react";
import { Button, Icon } from "semantic-ui-react";
import { Link } from "react-router-dom";

export default function AddEntityButton({linkTo, buttonText}) {
  return (
    <Button as={Link} to={linkTo} primary size="small" icon labelPosition="left" floated="right">
      <Icon name="plus" /> {buttonText}
    </Button>
  );
}
