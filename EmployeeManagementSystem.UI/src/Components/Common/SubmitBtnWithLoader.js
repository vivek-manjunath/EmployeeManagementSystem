import React from "react";
import { Button } from "semantic-ui-react";

export default function SubmitBtnWithLoader() {
  return (
    <Button type="submit" primary loading>
      Save
    </Button>
  );
}
