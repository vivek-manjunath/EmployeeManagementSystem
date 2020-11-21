import React from "react";
import { Button } from "semantic-ui-react";

export default function DomainCard(props) {
  return <Button icon>{props.name}</Button>;
}
