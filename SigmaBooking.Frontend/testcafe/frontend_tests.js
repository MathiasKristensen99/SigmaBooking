import { ClientFunction, Selector } from "testcafe";

fixture`Navbar tests`;

test.page("https://sigmabooking-68a9f.web.app/")("GoToHomeView", async (t) => {
  await t.click("#app > header > div > nav > div > button > span");
  await t.click("#navbarToggler > ul > li:nth-child(1) > a");
  const location = await ClientFunction(() => document.location.href);
  await t.expect(location()).contains("/home");
});

test.page("https://sigmabooking-68a9f.web.app/")(
  "GoToReservationView",
  async (t) => {
    await t.click("#app > header > div > nav > div > button > span");
    await t.click("#navbarToggler > ul > li:nth-child(2) > a");
    const location = await ClientFunction(() => document.location.href);
    await t.expect(location()).contains("/reservation");
  }
);

test.page("https://sigmabooking-68a9f.web.app/reservations")(
  "CreateNewReservation",
  async (t) => {
    await t
      .click("#kalender-div > button")
      .typeText("#addBooking > div > div > input:nth-child(2)", "jens")
      .typeText("#addBooking > div > div > input:nth-child(4)", "149149194914")
      .click("#addBooking > div > div > select")
      .click("#addBooking > div > div > select > option:nth-child(2)")
      .typeText(
        "#addBooking > div > div > input:nth-child(8)",
        "jens@gmail.com"
      )
      .typeText("#addBooking > div > div > input:nth-child(10)", "12:00")
      .typeText("#addBooking > div > div > input:nth-child(12)", "13:00")
      .click("#addBooking > div > div > div:nth-child(14) > input")
      .typeText(
        "#addBooking > div > div > input.form-control.antal_personer",
        "2"
      )
      .typeText(
        "#addBooking > div > div > input:nth-child(17)",
        "Manden skal have en kæmpe bøf med kæmpe løg, og kæmpe vin"
      )
      .click("#addBooking > div > div > div.modal-footer > button");
  }
);
