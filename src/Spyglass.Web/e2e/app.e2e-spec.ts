import { SpyglassPage } from './app.po';

describe('spyglass App', () => {
  let page: SpyglassPage;

  beforeEach(() => {
    page = new SpyglassPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
